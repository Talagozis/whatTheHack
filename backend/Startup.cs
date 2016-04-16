using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(whatTheHack.Startup))]
namespace whatTheHack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
