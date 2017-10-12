using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assemblify.Web.Startup))]
namespace Assemblify.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
