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
            AppStart();
        }

    // inicializa os roles e os utilizadores na BD
    private void AppStart()
    {
        ApplicationDbContext db = new ApplicationDbContext();

        var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));
        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

        // cria o role 'Admin' caso não exista
        if (!roleManager.RoleExists("Admin"))
        {
            var role = new ApplicationRole();
            role.Name = "Admin";
            roleManager.Create(role);

            // cria um utilizador 'Administrador'
            var user = new ApplicationUser();
            user.UserName = "admin@mail.pt";
            user.Email = "admin@mail.pt";
            string userPWD = "Teste123#$";
            var chkUser = userManager.Create(user, userPWD);

            // adiciona o utilizador ao role 'Administrador'
            if (chkUser.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }
        }

        // cria o role 'Premium' caso não exista
        if (!roleManager.RoleExists("Premium"))
        {
            var role = new ApplicationRole();
            role.Name = "Premium";
            roleManager.Create(role);
        }

        // cria o role 'Normal' caso não exista
        if (!roleManager.RoleExists("Normal"))
        {
            // cria o role
            var role = new ApplicationRole();
            role.Name = "Normal";
            roleManager.Create(role);
        }

        // cria a pasta das fotos dos utilizadores se esta não existir
        var pasta = System.Web.HttpContext.Current.Server.MapPath("~/Assets/images");
        Directory.CreateDirectory(pasta);
        }
    }
}