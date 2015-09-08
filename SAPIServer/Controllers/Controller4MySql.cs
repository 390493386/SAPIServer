using MySql.Data.MySqlClient;
using SiweiSoft.SAPIService.Core;
using SiweiSoft.SAPIService.Helper;
using System.Configuration;

namespace SiweiSoft.SAPIServer.Controllers
{
    /// <summary>
    /// 用户定义扩展Controller
    /// </summary>
    public class Controller4MySql : ControllerBase
    {
        /// <summary>
        /// MySql数据库连接上下文
        /// </summary>
        public ConnectionContext<MySqlConnection> CnContext { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public Controller4MySql()
        {
            ConnectionStringSettings cnStrSettings = ConfigurationManager.ConnectionStrings["MPA-JSYZ"];
            if (cnStrSettings != null)
                CnContext = new ConnectionContext<MySqlConnection>(cnStrSettings.ConnectionString);
            else
                Log.Comment(CommentType.Warn, "数据库连接（MPA-JSYZ）没有配置。");
        }
    }
}
