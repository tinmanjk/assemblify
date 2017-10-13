using Assemblify.Data.Models;
using Assemblify.Web.Providers.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Hosting;

namespace Assemblify.Web.Providers.Contracts
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        string CurrentUserUsername { get; }

        IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();

        bool IsInRole(string userId, string roleName);

        IdentityResult AddToRole(string userId, string roleName);
        IdentityResult ChangePassword(string userId, string currentPassword, string newPassword);

        //ApplicationUserManager UserManager { get; }

    }
}
