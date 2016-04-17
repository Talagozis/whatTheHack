using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(whatTheHack2.Startup))]
namespace whatTheHack2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
