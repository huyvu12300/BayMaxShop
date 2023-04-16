using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BayMaxShop.Startup))]
namespace BayMaxShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
