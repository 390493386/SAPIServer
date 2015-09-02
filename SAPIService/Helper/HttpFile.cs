namespace SiweiSoft.SAPIService.Helper
{
    public class HttpFile
    {
        /// <summary>
        /// 上传文件原始名字
        /// </summary>
        public string OriginName { get; set; }

        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public HttpFile() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="originName"></param>
        /// <param name="relativePath"></param>
        public HttpFile(string originName, string relativePath)
        {
            OriginName = originName;
            RelativePath = relativePath;
        }
    }
}
