using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class ErrorController: Controller
    {
        public ActionResult NotFound()
        {
            return this.View();
        }
    }
}