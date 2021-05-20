using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
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
        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostJob model)
        {
            var userid = User.Identity.GetUserId();
            DateTime date = DateTime.Now;
            try
            {
                model.IsStillAvilavble = true;
                model.IsAvilavbleAtWall = true;
                model.NumberOfSubmitted = 0;
                model.Rate = 1;
                model.CreationDate = date;
                model.UserName = User.Identity.GetUserName();
                
                model.UserId = userid;
                _db.PostJobs.Add(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

// GET: 
public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostJob model)
        {
            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == model.Id);

            DateTime date = DateTime.Now;

            if (post == null)
            {
                return HttpNotFound();
            }

            try
            {
                post.IsStillAvilavble = true;
                post.IsAvilavbleAtWall = true;
                post.JobBudget = model.JobBudget;
                post.JobType = model.JobType;
                post.CreationDate = date;

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {


            }

            return View();

        }

        // GET: 
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == id);
            var SavedPost = await _db.Saveds.FirstOrDefaultAsync(s => s.IdOfThePost == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _db.PostJobs.Remove(post);
                if (SavedPost != null)
                {
                    _db.Saveds.Remove(SavedPost);
                }

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
