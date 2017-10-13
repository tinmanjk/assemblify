using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Controllers
{
    public class PostController : Controller
    {

        [Authorize]
        public ActionResult Create()
        {
            //var model = this.factory.CreateCreateLogViewModel();

            return this.View();
        }
    }
}