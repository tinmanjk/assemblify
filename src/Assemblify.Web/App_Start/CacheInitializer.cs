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
        private ICachingProvider cachingProvider;
        private IUsersService usersService;

        public CacheInitializer(ICachingProvider cachingProvider, IUsersService usersService)
        {
            this.cachingProvider = cachingProvider;
            this.usersService = usersService;
        }
        
        public CacheInitializer():
            this(DependencyResolver.Current.GetService<ICachingProvider>(), 
                DependencyResolver.Current.GetService<IUsersService>())
        {

        }

        public void Initialize()
        {
            this.GenerateUsersCache();
        }

        public void GenerateUsersCache()
        {
            this.cachingProvider.GetOrAdd(GlobalConstants.CachingUserNames, 
                () => this.usersService.GetAllUserNames());
            
        }
    }
}