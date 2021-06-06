using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Utility;

namespace WebApplication1.Areas.FreeLancer.Controllers
{
    [Authorize(Roles = SD.FreelancerUser)]
    public class MySavedPostController : Controller
    {
        public ApplicationDbContext _db;
        private readonly ISavedPostsRepository savedPostsRepository;
        private readonly IPostJobRepository postJobRepository;
        public MySavedPostController()
        {
            _db = new ApplicationDbContext();
            savedPostsRepository = new SavedPostsRepository(new ApplicationDbContext());
            postJobRepository = new PostJobRepository(new ApplicationDbContext());
        }

        // GET: FreeLancer/MySavedPost
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();          

            var mySaveds = from s in _db.Saveds
                           where s.IdOfTheUser == userID
                           select s;

            var Posts = from p in _db.PostJobs
                        join u in mySaveds
                        on p.Id equals u.IdOfThePost
                        select p;

            return View(Posts.Where(p=>p.IsAvilavbleAtWall == true));
        }


        public ActionResult Saveds()
        {
            string userID = User.Identity.GetUserId();
            var mySaveds = from s in _db.Saveds
                           where s.IdOfTheUser == userID
                           select s;
            return View(mySaveds);

        }

        // GET: FreeLancer/MySavedPost/Add
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = postJobRepository.GetPostJobById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: FreeLancer/MySavedPost/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(int id)
        {
            var postFromDb = postJobRepository.GetPostJobById(id);
            var userID = User.Identity.GetUserId();
            Saved SavePost = new Saved();
            if (postFromDb == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                SavePost.IdOfThePost = id;
                SavePost.IsMarked = true;
                SavePost.IdOfTheUser = userID;
                bool exist = await savedPostsRepository.CkeckIfthePostSavedBeforeAsync(id, userID);
                if (exist)
                {
                    ViewBag.ExistSavedPost = "You have already added it";
                }
                else
                {
                    savedPostsRepository.SavePostAtMyPage(SavePost);
                    await savedPostsRepository.SaveChangeAsync();
                    return RedirectToAction("Index", "Posts", new { area = "" });
                }
                
            }
            return View();                
           
        }



        // GET: FreeLancer/MySavedPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var post = postJobRepository.GetPostJobById(id);
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
            var savedpost = savedPostsRepository.GetSavedsById(id);
            if (savedpost == null)
            {
                return HttpNotFound();
            }
            try
            {
                savedPostsRepository.DeleteSavedPost(savedpost);
                await savedPostsRepository.SaveChangeAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
