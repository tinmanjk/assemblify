[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Assemblify.Web.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Assemblify.Web.App_Start.NinjectConfig), "Stop")]

namespace Assemblify.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Data.Repositories;
    using Data.SaveContext;
    using System.Data.Entity;
    using Data;
    using Services.Contracts;
    using AutoMapper;
    using System.Reflection;
    using System.IO;
    using Providers.Contracts;
    using Providers;
    using Routes;
    using Infrastructure.Providers.Contracts;
    using Infrastructure.Factories;
    using Ninject.Extensions.Factory;
    using Data.Models.Abstracts;

    public static class NinjectConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                // Service Locator DependencyResolver works without this for some reason
                System.Web.Mvc.DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(kernel));

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            // Data Models
            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(DataModel))
                 .SelectAllClasses()
                 .BindToSelf();
            });

            // Services
            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IServiceNinjectRegistry))
                 .SelectAllClasses()
                 .BindDefaultInterface()
                 .Configure(s => s.InRequestScope());
            });

            // Web Providers
            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IWebProviderNinjectRegistry))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            // Infrastructure Providers
            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IInfrastructureProviderNinjectRegistry))
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });

            // Factories
            kernel.Bind<IPostFactory>().ToFactory().InSingletonScope();
            kernel.Bind<IUserFactory>().ToFactory().InSingletonScope();

            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InRequestScope();
            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind<ISaveContext>().To<SaveContext>();

            kernel.Bind<IMapper>().ToMethod(x=>Mapper.Instance).InSingletonScope();
        }
    }
}
