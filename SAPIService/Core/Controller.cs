using System.Collections.Generic;
using System.Data.Common;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class Controller
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
        public virtual Controller Clone()
        {
            return (Controller)this.MemberwiseClone();
        }

        /// <summary>
        /// 获取数据库连接上下文信息
        /// </summary>
        /// <typeparam name="TConnection"></typeparam>
        /// <returns></returns>
        public virtual ConnectionContext<TConnection> GetConnectionContext<TConnection>()
            where TConnection : DbConnection, new()
        {
            ConnectionContext<TConnection> conCtx = null;
            if (SapiService.ConnectionString != null)
                conCtx = new ConnectionContext<TConnection>(SapiService.ConnectionString);
            return conCtx;
        }

        /// <summary>
        /// 根据参数名字获取参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetParameterByName<T>(string name)
        {
            return Parameters.ContainsKey(name) ? (T)Parameters[name] : default(T);
        }
    }
}
