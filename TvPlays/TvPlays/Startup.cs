using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TvPlays.Startup))]
namespace TvPlays
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
