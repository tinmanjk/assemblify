using Assemblify.Data;
using Assemblify.Data.Migrations;
using Assemblify.Infrastructure.Mapping;
using Assemblify.Services;
using Assemblify.Services.Contracts;
using Assemblify.Web;
using Assemblify.Web.App_Start;
using Assemblify.Web.Providers.Contracts;
using Assemblify.Web.Routes;
using Ninject;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Assemblify.Web
{
    public class MvcApplication : HttpApplication
    {
        //protected IDependencyResolver dependencyResolver;

        protected void Application_Start()
        {
            // bez WebForms
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // moje DropCreate za testovite celi
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MsSqlDbContext>());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            var cachheInitializer = new CacheInitializer();
            cachheInitializer.Initialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var routeConfig = new RouteConfig();
            routeConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
