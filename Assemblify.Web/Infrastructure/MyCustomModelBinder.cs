using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.Infrastructure
{
    public class MyCustomModelBinder : DefaultModelBinder
    {
        //public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var model = new PostViewModel();

        //    model.Title = controllerContext.HttpContext.Request.Form["firstName"];
        //}
    }
}