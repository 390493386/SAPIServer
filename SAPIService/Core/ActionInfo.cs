using System.Reflection;

namespace SiweiSoft.SAPIService.Core
{
    internal class ActionInfo
    {
        /// <summary>
        /// Action
        /// </summary>
        public MethodInfo Action { get; set; }

        /// <summary>
        /// Action特性，是否需要授权
        /// </summary>
        public bool NeedAuthorize { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ActionInfo()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="needAuthorize"></param>
        public ActionInfo(MethodInfo action, bool needAuthorize)
        {
            Action = action;
            NeedAuthorize = needAuthorize;
        }
    }
}
