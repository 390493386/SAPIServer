using System.Collections.Generic;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class ControllerBase
    {
        /// <summary>
        /// 服务器配置
        /// </summary>
        public Dictionary<string, object> ServerConfigs
        {
            protected get;
            set;
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            protected get;
            set;
        }

        /// <summary>
        /// 会话
        /// </summary>
        public Session Session
        {
            protected get;
            set;
        }

        /// <summary>
        /// 重写克隆方法
        /// </summary>
        /// <returns></returns>
        public virtual ControllerBase Clone()
        {
            return (ControllerBase)this.MemberwiseClone();
        }
    }
}
