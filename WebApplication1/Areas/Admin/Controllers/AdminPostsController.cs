using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AdminPostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminPostsController()
        {
            _db = new ApplicationDbContext(); 
        }
        // GET: Admin/AdminPosts
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            return View(_db.PostJobs.Where(p => p.UserId == userid));
        }

        
    }
}
