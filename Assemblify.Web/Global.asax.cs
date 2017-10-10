using Assemblify.Data;
using Assemblify.Data.Migrations;
using Assemblify.Infrastructure.Mapping;
using Assemblify.Web;
using Assemblify.Web.App_Start;
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
        protected void Application_Start()
        {
            // bez WebForms
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // moje DropCreate za testovite celi
            //Database.SetInitializer(new DropCreateDatabaseAlways<MsSqlDbContext>());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
