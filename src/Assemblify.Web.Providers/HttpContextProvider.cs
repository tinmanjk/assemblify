using Assemblify.Web.Providers.Contracts;
using Microsoft.Owin;

using System.Web;
using System.Web.Caching;

using System.Security.Principal;
using Microsoft.AspNet.Identity.Owin;


namespace Assemblify.Web.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContext CurrentHttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public IOwinContext CurrentOwinContext
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }

        public IIdentity CurrentIdentity
        {
            get
            {
                return HttpContext.Current.User.Identity;
            }
        }

        public TManager GetUserManager<TManager>()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<TManager>();
        }

        public Cache ContextCache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }

    }
}
