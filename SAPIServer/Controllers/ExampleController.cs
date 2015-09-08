using SiweiSoft.SAPIService.Core;
using System;

namespace SiweiSoft.SAPIServer.Controllers
{
    public class ExampleController : Controller4MySql
    {
        [Action("TEST", needAuthorize: false)]
        public ActionResult Test()
        {
            DAOProcessCode code;
            CnContext.OpenConnection(out code);
            var ar = new ActionResult();
            ar.Result.Add("code", 200);
            ar.Result.Add("message", "Success");
            return ar;
        }
    }
}
