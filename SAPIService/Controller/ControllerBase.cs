using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Dao;
using System.Collections.Generic;
using System.Data.Common;

namespace SiweiSoft.SAPIService.Controller
{
    public abstract class ControllerBase<TConnectionContext, TConnection>
        where TConnection : DbConnection
        where TConnectionContext : ConnectionContext<TConnection>
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
        /// 数据库连接上下文
        /// </summary>
        public TConnectionContext ConnectionContext
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
