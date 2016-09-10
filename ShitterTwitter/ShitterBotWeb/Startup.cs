using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShitterBotWeb.Startup))]
namespace ShitterBotWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
