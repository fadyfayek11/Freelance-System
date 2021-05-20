using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Areas.FreeLancer.Controllers
{
    [Authorize(Roles = SD.FreelancerUser)]
    public class MySavedPostController : Controller
    {
        public ApplicationDbContext _db;
        public MySavedPostController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: FreeLancer/MySavedPost
        public ActionResult Index()
        {
            var mySaveds = from p in _db.PostJobs
                           join u in _db.Saveds
                           on p.Id equals u.IdOfThePost
                           select p;
            return View(mySaveds);
        }



        // GET: FreeLancer/MySavedPost/Add
        public async Task<ActionResult> Add(int id)
        {
            
            var post = await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: FreeLancer/MySavedPost/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(int id, Saved SavePost)
        {
            var postFromDb = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == id);
            SavePost = new Saved();
            if (postFromDb == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                SavePost.IdOfThePost = id;
                SavePost.IsMarked = true;
                SavePost.IdOfTheUser = postFromDb.UserId;
                bool exist = await _db.Saveds.FirstOrDefaultAsync(s=>s.IdOfThePost == id && s.IdOfTheUser == postFromDb.UserId) != null;
                if (exist)
                {
                    ViewBag.ExistSavedPost = "You have already added it";
                }
                else
                {
                    _db.Saveds.Add(SavePost);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Posts", new { area = "" });
                }
                
            }
            return View();                
           
        }



        // GET: FreeLancer/MySavedPost/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: FreeLancer/MySavedPost/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var savedpost = await _db.Saveds.FirstOrDefaultAsync(s=>s.IdOfThePost == id);
            if (savedpost == null)
            {
                return HttpNotFound();
            }
            try
            {
                _db.Saveds.Remove(savedpost);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
