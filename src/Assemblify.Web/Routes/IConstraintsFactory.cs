using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using System.Web.Routing;

namespace Assemblify.Web.Routes
{
    public interface IConstraintsFactory
    {
        IRouteConstraint CreateUserNameConstraint(IHttpCachingProvider cachingProvider, IUsersService usersService);
    }
}