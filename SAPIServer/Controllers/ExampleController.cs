using MySql.Data.MySqlClient;
using SiweiSoft.SAPIService.Core;
using System;

namespace SiweiSoft.SAPIServer.Controllers
{
    public class ExampleController : Controller
    {
        [Action("TEST", needAuthorize: false)]
        public ActionResult Test()
        {
            ConnectionContext<MySqlConnection> conCtx = GetConnectionContext<MySqlConnection>();

            DAOProcessCode code;
            conCtx.OpenConnection(out code);
            var ar = new ActionResult();
            ar.Result.Add("code", 200);
            ar.Result.Add("message", "Success");
            return ar;
        }
    }
}
