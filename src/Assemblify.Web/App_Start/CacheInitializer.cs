using Assemblify.Common;
using Assemblify.Services.Contracts;
using Assemblify.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assemblify.Web.App_Start
{
    public class CacheInitializer
    {
        private IHttpCachingProvider cachingProvider;
        private IUsersService usersService;

        public CacheInitializer(IHttpCachingProvider cachingProvider, IUsersService usersService)
        {
            this.cachingProvider = cachingProvider;
            this.usersService = usersService;
        }
        
        public CacheInitializer():
            this(DependencyResolver.Current.GetService<IHttpCachingProvider>(), 
                DependencyResolver.Current.GetService<IUsersService>())
        {

        }

        public void Initialize()
        {
            this.GenerateUsersCache();
        }

        private void GenerateUsersCache()
        {
            this.cachingProvider.GetOrAdd(GlobalConstants.CachingUserNames, 
                () => this.usersService.GetAllUserNames());
            
        }
    }
}