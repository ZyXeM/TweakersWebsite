using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TweakersRemake.Startup))]
namespace TweakersRemake
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
