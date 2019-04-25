using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TvPlays.Models;

[assembly: OwinStartupAttribute(typeof(TvPlays.Startup))]
namespace TvPlays
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            iniciaAplicacao();
        }

        //Inicar aplicacao com as Roles e o criar um utilizador Admin
        private void iniciaAplicacao()
        {

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //----------------------- USER - ADMIN ---------------------------------------------------------------------------------------------------------------------------------------
            //---------- admin@mail.pt ------------------
            //---------- -Admin1 ------------------------
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Admin'
            if (!roleManager.RoleExists("Admin"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // criar a Role 'NormalUser'
            if (!roleManager.RoleExists("NormalUser"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "NormalUser";
                roleManager.Create(role);
            }

            // criar um utilizador 'Admin'
            var user = new ApplicationUser();
            user.UserName = "admin@mail.pt";
            user.Email = "admin@mail.pt";
            string userPWD = ".Admin1";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Admin
            if (chkUser.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
