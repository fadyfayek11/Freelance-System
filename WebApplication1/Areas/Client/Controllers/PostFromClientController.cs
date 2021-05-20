using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Areas.Client.Controllers
{
    [Authorize(Roles = SD.ClientUser)]
    public class PostFromClientController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostFromClientController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Client/PostFromClient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/PostFromClient/Create
        [HttpPost]
        public async Task<ActionResult> Create(PostJob model)
        {
            var userid = User.Identity.GetUserId();
            try
            {
                model.IsStillAvilavble = false;
                model.IsAvilavbleAtWall = true;
                model.NumberOfSubmitted = 0;
                model.Rate = 1;

                model.UserId = userid;
                _db.PostJobs.Add(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index","Home",new { area=""});
            }
            catch
            {
                return View();
            }
        }

       
    }
}
