using SiweiSoft.SAPIService.Controller;
using SiweiSoft.SAPIService.Dao.MySql;

namespace SiweiSoft.SAPIServer.Controllers
{
    /// <summary>
    /// 支持mysql数据库连接
    /// </summary>
    public class ControllerMpa : ControllerBase<MySqlConnectionContext,MySqlConnection>
    {
        public MySqlConnectionContext ConnectionContext { get; set; }
    }
}
