using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThriftyWeb.Startup))]
namespace ThriftyWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
