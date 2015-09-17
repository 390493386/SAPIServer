using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

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
        /// 构造方法
        /// </summary>
        public ActionResult()
        {
            Headers = new List<string>();
            Result = new Dictionary<string, object>();
        }

        /// <summary>
        /// 获取返回结果
        /// </summary>
        /// <returns></returns>
        public virtual string GetResultString()
        {
            return new JavaScriptSerializer().Serialize(this.Result);
        }
    }

    /// <summary>
    /// 未授权请求
    /// </summary>
    public class ActionNotAuthorized : ActionResult
    {
        public ActionNotAuthorized()
        {
            Result = new Dictionary<string, object>();
            Result.Add("code", -1);
            Result.Add("message", "Request not authorized.");
        }
    }
}
