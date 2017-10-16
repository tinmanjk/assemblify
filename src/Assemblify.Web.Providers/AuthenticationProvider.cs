using Assemblify.Web.Providers.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assemblify.Data.Models;
using Microsoft.AspNet.Identity.Owin;
using Assemblify.Web.Providers.Managers;
using System.Web.Hosting;
using Assemblify.Infrastructure.Providers.Contracts;

namespace Assemblify.Web.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {

        private readonly IDateTimeProvider dateTimeProvider;

        private readonly IHttpContextProvider httpContextProvider;


        public AuthenticationProvider(IDateTimeProvider dateTimeProvider, IHttpContextProvider httpContextProvider)
        {
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

            if (httpContextProvider == null)
            {
                throw new ArgumentNullException(nameof(httpContextProvider));
            }

            this.dateTimeProvider = dateTimeProvider;
            this.httpContextProvider = httpContextProvider;
        }

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationSignInManager>();
            }
        }

        // to be switched to protected
        public ApplicationUserManager UserManager
        {
            get
            {
                return this.httpContextProvider.GetUserManager<ApplicationUserManager>();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.IsAuthenticated;
            }
        }

        public string CurrentUserId
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserId();
            }
        }

        public string CurrentUserUsername
        {
            get
            {
                return this.httpContextProvider.CurrentIdentity.GetUserName();
            }
        }

        public IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser)
        {
            var result = this.UserManager.Create(user, password);

            if (result.Succeeded)
            {
                this.SignInManager.SignIn(user, isPersistent, rememberBrowser);
            }

            return result;
        }

        public IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return this.UserManager.ChangePassword(userId, currentPassword, newPassword);

        }

        public SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout)
        {
            return this.SignInManager.PasswordSignIn(email, password, rememberMe, shouldLockout);
        }

        public void SignOut()
        {
            this.httpContextProvider.CurrentOwinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public bool IsInRole(string userId, string roleName)
        {
            return userId != null && this.UserManager.IsInRole(userId, roleName);
        }

        public IdentityResult AddToRole(string userId, string roleName)
        {
            return this.UserManager.AddToRole(userId, roleName);
        }
    }
}
