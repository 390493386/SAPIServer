using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SiweiSoft.SAPIService.Core
{
    public class ActionResult
    {
        /// <summary>
        /// 需要添加到响应包中的头
        /// </summary>
        public List<string> Headers { get; set; }

        /// <summary>
        /// 文件流，文件下载请求时使用
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// 请求响应数据
        /// </summary>
        public Dictionary<string, object> Result { get; set; }

        /// <summary>
        /// 获取返回结果
        /// </summary>
        /// <returns></returns>
        public virtual string GetResultString()
        {
            return JsonConvert.SerializeObject(this.Result);
        }
    }

    /// <summary>
    /// 未授权请求
    /// </summary>
    public class ActionNotAuthorized : ActionResult
    {
        public override string GetResultString()
        {
            //此处作优化，直接返回JSON字符串，不需要序列化
            return "{\"code\":-1,\"message\":\"Request not authorized.\"}";
        }
    }

    /// <summary>
    /// 数据请求
    /// </summary>
    public class DataActionResult : ActionResult
    {
        public DataActionResult()
        {
            Result = new Dictionary<string, object>();

            Result.Add("code", 200);
            Result.Add("message", "success");
        }

        public DataActionResult(object data)
        {
            Result = new Dictionary<string, object>();

            Result.Add("code", 200);
            Result.Add("message", "success");
            Result.Add("data", data);
        }

        public DataActionResult(List<string> headers, object data)
        {
            Headers = headers;

            Result = new Dictionary<string, object>();
            Result.Add("code", 200);
            Result.Add("message", "success");
            Result.Add("data", data);
        }
    }

    /// <summary>
    /// 文件下载
    /// </summary>
    public class FileActionResult : ActionResult
    {
        public FileActionResult(Stream fileStream)
        {
            Stream = fileStream;
        }

        public FileActionResult(List<string> headers, Stream fileStream)
        {
            Headers = headers;
            Stream = fileStream;
        }
    }
}
