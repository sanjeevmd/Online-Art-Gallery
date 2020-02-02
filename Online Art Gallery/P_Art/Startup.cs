using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(P_Art.Startup))]
namespace P_Art
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
