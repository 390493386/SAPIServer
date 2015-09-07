using System;
using System.IO;
using System.Net;
using System.Text;

namespace SiweiSoft.SAPIService.Helper
{
    public class HttpFileUtilities
    {
        /// <summary>
        /// 保存请求中的文件流
        /// </summary>
        /// <param name="fileUploadRequest"></param>
        /// <param name="rootPath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static HttpFile SaveFile(HttpListenerRequest fileUploadRequest, string rootPath, Encoding encoding)
        {
            HttpFile uploadFile = null;

            //判断上传文件大小，默认大小不能超过50M
            long contentLength = Convert.ToInt64(fileUploadRequest.Headers["Content-Length"]);
            if (contentLength > 1024 * 1024 * 50)
                Log.Comment(CommentType.Error, "上传文件大小不能超过50M，文件上传失败！");
            else if (rootPath == null)
                Log.Comment(CommentType.Error, "文件存放路径未设置，文件上传失败！");
            else
            {
                //获取文件分隔符
                string boundary = fileUploadRequest.ContentType.Split(';')[1];
                int endBoundaryLength = boundary.Length - 6;

                byte[] buffer = new byte[1024]; //缓冲数组，可设定任意大小，设定过小可能导致文件上传速度慢
                int headLength = 0; //上传文件头信息长度
                int singleReadLength = 0; //单次输入流中读取的长度
                int bufferLength = 0; //当前buffer使用的长度
                bufferLength = singleReadLength = fileUploadRequest.InputStream.Read(buffer, 0, buffer.Length);
                int startPos = -1;

                byte[] headByte = new byte[2048]; //文件头长度默认大小，假定文件头部最大长度不超过2048
                if (singleReadLength < 1)
                    Log.Comment(CommentType.Error, "文件流读取失败！");

                while (singleReadLength > 0)
                {
                    //找到文件主体部分分隔符，两个换行符，字节数组为{13,10,13,10}
                    startPos = IndexOf(buffer, bufferLength, new byte[4] { 13, 10, 13, 10 });

                    if (startPos >= 0)
                    {
                        Array.Copy(buffer, 0, headByte, headLength, startPos);
                        headLength += startPos;

                        string headString = encoding.GetString(headByte, 0, headLength);
                        string contentDisposition = headString.Split('\n')[1];

                        uploadFile = new HttpFile();
                        int posFileName = contentDisposition.IndexOf("filename=");
                        uploadFile.OriginName = contentDisposition.Substring(posFileName + 10, contentDisposition.Length - posFileName - 12);
                        int posPoint = uploadFile.OriginName.LastIndexOf('.');
                        string fileExtention = posPoint > -1 ? uploadFile.OriginName.Substring(posPoint) : null;  //文件扩展名

                        //生成guid命名文件，防止服务器文件重名
                        string subFolder = DateTime.Now.ToShortDateString();
                        uploadFile.RelativePath = Path.Combine(subFolder, Guid.NewGuid().ToString() + fileExtention);

                        string completePath = Path.Combine(rootPath, subFolder);
                        if (!Directory.Exists(completePath))
                            Directory.CreateDirectory(completePath);
                        using (FileStream saveStream = new FileStream(Path.Combine(rootPath, uploadFile.RelativePath), FileMode.Create, FileAccess.Write))
                        {
                            //数据流总长度，去掉头部和分隔符和文件结束边界和当前读取的文件数据部分，就是剩余要读取的文件数据部分长度
                            //int remainFileLength = (int)contentLength - (headLength + 4 + endBoundaryLength) - (bufferLength - startPos - 4);
                            int remainFileLength = (int)contentLength - headLength - endBoundaryLength - bufferLength + startPos;
                            int fileStartPos = startPos + 4;
                            saveStream.Write(buffer, fileStartPos, bufferLength - fileStartPos);
                            while (remainFileLength > 0)
                            {
                                singleReadLength = fileUploadRequest.InputStream.Read(buffer, 0, buffer.Length < remainFileLength ? buffer.Length : remainFileLength);
                                saveStream.Write(buffer, 0, singleReadLength);
                                remainFileLength -= singleReadLength;
                            }
                            //此时，文件数据部分已经读取完毕，数据流中还有文件结束边界没读取，此时没必要继续读，直接把singleReadLength设为0，结束循环
                            singleReadLength = 0;
                        }
                    }
                    else
                    {
                        //最后三个字符中可能含有分隔符，所以拷贝长度为当前读取长度减3
                        int copyLength = bufferLength - 3;
                        Array.Copy(buffer, 0, headByte, headLength, copyLength);
                        headLength += copyLength;

                        //最后三个字符不足匹配长度，放到新的缓冲里面继续匹配
                        buffer[0] = buffer[bufferLength - 3];
                        buffer[1] = buffer[bufferLength - 2];
                        buffer[2] = buffer[bufferLength - 1];
                        singleReadLength = fileUploadRequest.InputStream.Read(buffer, 3, buffer.Length - 3);
                        bufferLength = singleReadLength + 3;
                    }
                }
            }

            return uploadFile;
        }

        /// <summary>
        /// 获取指定字节数组的位置
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        /// <param name="seachPattern"></param>
        /// <returns></returns>
        private static int IndexOf(byte[] buffer, int length, byte[] seachPattern)
        {
            int index = -1;
            for (int i = 0; i < length - seachPattern.Length + 1; ++i)
            {
                bool match = true;
                for (int j = 0; j < seachPattern.Length && match; ++j)
                {
                    match = buffer[i + j] == seachPattern[j];
                }
                if (match)
                    index = i;
            }
            return index;
        }
    }
}
