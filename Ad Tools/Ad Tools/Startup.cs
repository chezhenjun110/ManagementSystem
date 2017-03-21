using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ad_Tools.Startup))]
namespace Ad_Tools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
