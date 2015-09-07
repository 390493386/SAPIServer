using SiweiSoft.SAPIService.Core;
using System;

namespace SiweiSoft.SAPIServer.Controllers
{
    public class ExampleController : ControllerBase
    {
        [Action("TEST", needAuthorize: false)]
        public ActionResult Test()
        {
            var ar = new ActionResult();
            ar.Result.Add("code", 200);
            ar.Result.Add("message", "Success");
            return ar;
        }
    }
}
