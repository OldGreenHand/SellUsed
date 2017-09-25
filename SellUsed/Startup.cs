using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SellUsed.Startup))]
namespace SellUsed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
