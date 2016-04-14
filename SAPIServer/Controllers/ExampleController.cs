using MySql.Data.MySqlClient;
using SiweiSoft.SAPIService.Core;
using System;

namespace SiweiSoft.SAPIServer.Controllers
{
    public class ExampleController : ControllerBase
    {
        [Action("TEST", needAuthorize: false)]
        public ActionResult Test()
        {
            ConnectionContext<MySqlConnection> conCtx = GetConnectionContext<MySqlConnection>();

            DAOProcessCode code;
            conCtx.OpenConnection(out code);

            var persons = new[]
            {
                new { Name = "zhanhui", Age = 27},
                new { Name = "yangzhenzhen", Age = 25 }
            };
            return new DataActionResult(persons);
        }
    }
}
