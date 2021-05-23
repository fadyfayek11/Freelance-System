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
    public class PostRequestController : Controller
    {

        private readonly ApplicationDbContext _db;
        public PostRequestController()
        {
            _db = new ApplicationDbContext();     
        }
        // GET: Admin/PostRequest
        public async Task<ActionResult> Index()
        {

            foreach (var item in _db.PostJobs.Where(p => p.IsNotificationOfPostsRequestSeen == null))
            {
                item.IsNotificationOfPostsRequestSeen = true;                
            }
           await _db.SaveChangesAsync();

            return View(_db.PostJobs.Where(p=>p.IsStillAvilavble == false).ToList());
        }
       


        // GET: Admin/PostRequest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post =  await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id==id);
            if (post == null)
            {
                return HttpNotFound();
            }
            
            return View(post);
        }

        // POST: Admin/PostRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostJob model)
        {    
            var post = await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id==model.Id);

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

        // GET: Admin/PostRequest/Delete/5
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

        // POST: Admin/PostRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {

            var post = await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id == id);
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
