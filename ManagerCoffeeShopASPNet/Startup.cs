using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagerCoffeeShopASPNet.Startup))]
namespace ManagerCoffeeShopASPNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
