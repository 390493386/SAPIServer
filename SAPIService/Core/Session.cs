using System;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// 用户会话
    /// </summary>
    public abstract class SessionBase
    {
        /// <summary>
        /// 会话过期日期
        /// </summary>
        private DateTime ExpireDate;

        /// <summary>
        /// 会话是否授权
        /// </summary>
        public bool IsAuthorized
        {
            set;
            get;
        }

        /// <summary>
        /// 重设会话过期日期
        /// </summary>
        /// <param name="seconds"></param>
        internal void ResetExpireDate(int seconds)
        {
            ExpireDate = DateTime.Now.AddSeconds(seconds);
        }

        /// <summary>
        /// 会话是否过期
        /// </summary>
        /// <returns></returns>
        internal bool IsSessionExpired()
        {
            return DateTime.Now > ExpireDate;
        }
    }
}
