using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Client.Controllers
{
    public class ClientPostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ClientPostsController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Client/ClientPosts
        public ActionResult Index()
        {

            var userid = User.Identity.GetUserId();           
            return View(_db.PostJobs.Where(p=>p.UserId == userid));
        }

        // GET: Client/ClientPosts/Edit/5
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

        // POST: Client/ClientPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostJob model)
        {
            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == model.Id);
            model.IsStillAvilavble = true;
            model.IsAvilavbleAtWall = true;
            
            if (post == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                post.IsStillAvilavble = model.IsStillAvilavble;
                post.IsAvilavbleAtWall = model.IsAvilavbleAtWall;
                post.JobBudget = model.JobBudget;
                post.JobType = model.JobType;
              
                post.UserName = model.UserName;


                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: Client/ClientPosts/Delete/5
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
        // POST: Client/ClientPosts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {

            var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                _db.PostJobs.Remove(post);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();

        }
    }
}
