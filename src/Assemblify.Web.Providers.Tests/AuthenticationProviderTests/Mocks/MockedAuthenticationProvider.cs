using Assemblify.Infrastructure.Providers.Contracts;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Providers.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Tests.AuthenticationProviderTests.Mocks
{
    public class MockedAuthenticationProvider : AuthenticationProvider
    {
        public MockedAuthenticationProvider(IDateTimeProvider dateTimeProvider, IHttpContextProvider httpContextProvider)
            : base(dateTimeProvider, httpContextProvider)
        {
        }

        public ApplicationSignInManager GetSignInManager()
        {
            return this.SignInManager;
        }

        public ApplicationUserManager GetUserManager()
        {
            return this.UserManager;
        }
    }
}
