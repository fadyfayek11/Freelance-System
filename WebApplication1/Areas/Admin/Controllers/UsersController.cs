using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private ApplicationUserManager _userManager;
        public UsersController()
        {
            _db = new ApplicationDbContext();
        }
        public UsersController(ApplicationUserManager manager)
        {
           
            UserManager = manager;          
        }
       

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var myId = User.Identity.GetUserId();
            ViewBag.ProposalRequest = _db.Proposals.Where(p => p.ClientId == myId && p.IsAccepted == null).Count();

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ViewBag.CountRequest = _db.PostJobs.Where(p => p.IsStillAvilavble == false).Count();
            return View(_db.Users.Where(u => u.Id != claim.Value).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            string role = Request.Form["rdUserRole"].ToString();
            var user = new ApplicationUser
            {
                FirstName = model.FirsttName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Photo = "profile.png",
                Role = role
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (ModelState.IsValid && result.Succeeded)
            {
                if (role == SD.ClientUser)
                {
                    await UserManager.AddToRoleAsync(user.Id, SD.ClientUser);
                }
                else
                {
                    if (role == SD.FreelancerUser)
                    {
                        await UserManager.AddToRoleAsync(user.Id, SD.FreelancerUser);
                    }
                    else
                    {
                        await UserManager.AddToRoleAsync(user.Id, SD.AdminUser);
                    }
                    
                }
                
                _db.Users.Add(user);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var user =  _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(ApplicationUser model)
        {
            var user =await _db.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = "No user";
                return HttpNotFound();
            }
            if (user.Photo != null)
            {
                string path = Server.MapPath("~/ProfilesImages/") + user.Photo;
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            

             _db.Users.Remove(user);
            await _db.SaveChangesAsync();            
            return RedirectToAction("Index");
        }
    }
}