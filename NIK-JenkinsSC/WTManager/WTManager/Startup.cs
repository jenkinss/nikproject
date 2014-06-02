using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WTManager.Startup))]
namespace WTManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
