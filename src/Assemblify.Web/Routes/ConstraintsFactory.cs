using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Routes.Contraints;

namespace Assemblify.Web.Routes
{
    public class ConstraintsFactory : IConstraintsFactory
    {
        public IRouteConstraint CreateUserNameConstraint(ICachingProvider cachingProvider)
        {
            return new UserNameConstraint(cachingProvider);
        }
    }
}