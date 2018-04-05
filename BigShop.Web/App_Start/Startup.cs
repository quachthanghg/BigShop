using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BigShop.Data;
using BigShop.Data.Infrastructure;
using BigShop.Data.Repositories;
using BigShop.Service.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BigShop.Web.App_Start.Startup))]

namespace BigShop.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigAutofac(app);
        }
        public void ConfigAutofac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // register controller.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Register your Web API Controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BigShopDbContext>().AsSelf().InstancePerRequest();

            //ASP.Net Identity
            //builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            //builder.Register(x => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            //builder.Register(x => app.GetDataProtectionProvider()).InstancePerRequest();


            // Repositories
            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).Assembly)
                .Where(p => p.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).Assembly)
                .Where(p => p.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();


            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            // set WebAPi
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}
