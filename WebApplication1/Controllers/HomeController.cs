using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;
        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.ProposalRequest = _db.Proposals.Where(p => p.ClientId == userid && p.IsAccepted == null).Count();

            ViewBag.CountRequest = _db.PostJobs.Where(p => p.IsStillAvilavble == false).Count();
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}