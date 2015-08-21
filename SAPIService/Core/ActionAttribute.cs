using System;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// Action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
        /// <summary>
        /// Action别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 是否需要授权（登陆）
        /// </summary>
        public bool NeedAuthorize { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="alias">Action别名</param>
        /// <param name="needAuthorize">是否需要授权</param>
        public ActionAttribute(string alias, bool needAuthorize = true)
        {
            Alias = alias;
            NeedAuthorize = needAuthorize;
        }
    }
}
