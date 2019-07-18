using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.IO;
using TvPlays.Models;

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