using MySql.Data.MySqlClient;
using SiweiSoft.SAPIService.Controller;
using SiweiSoft.SAPIService.Dao.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiweiSoft.SAPIService.Controller.MySql
{
    public class MySqlController : ControllerBase<MySqlConnectionContext, MySqlConnection>
    {
        public MySqlConnectionContext ConnectionContext { get; set; }
    }
}
