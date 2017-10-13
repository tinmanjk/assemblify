using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Assemblify.Web.ViewModels;
using Assemblify.Web.ViewModels.Manage;
using Assemblify.Web.Providers.Managers;
using Assemblify.Web.Providers.Contracts;

namespace Assemblify.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        private IAuthenticationProvider authenticationProvider;

        public ProfileController(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }


        //
        // GET: /Manage/Index
        public ActionResult Index(string message)
        {
            this.ViewBag.StatusMessage = message;
            var userId = authenticationProvider.CurrentUserId;
            var model = new IndexViewModel
            {
                HasPassword = true
            };
            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = this.authenticationProvider.ChangePassword(this.authenticationProvider.CurrentUserId, 
                model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                return RedirectToAction("Index", new { Message = "Successfully changed password" });
            }
            AddErrors(result);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}