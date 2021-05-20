using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;
using WebApplication1.Utility;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //AddRole();            
        }
       
        //public async void AddRole()
        //{
        //  var _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

        //    if (!await _roleManager.RoleExistsAsync(SD.AdminUser))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole(SD.AdminUser));
        //    }
        //    if (!await _roleManager.RoleExistsAsync(SD.ClientUser))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole(SD.ClientUser));
        //    }
        //    if (!await _roleManager.RoleExistsAsync(SD.FreelancerUser))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole(SD.FreelancerUser));
        //    }
            
        //}
    }
}
