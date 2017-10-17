using Assemblify.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assemblify.Web.Filters;

namespace Assemblify.Web.Areas.Administration.Controllers
{
    [AdministrationAreaAuthorizationFilter(Roles = GlobalConstants.AdministratorRoleName)]

    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

    }
}