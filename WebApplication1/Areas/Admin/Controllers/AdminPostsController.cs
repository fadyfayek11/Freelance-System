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

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    public class AdminPostsController : Controller
    {
        private readonly IPostJobRepository postJobRepository;
        private readonly ISavedPostsRepository savedPostsRepository;
        public AdminPostsController()
        {
            postJobRepository = new PostJobRepository(new ApplicationDbContext());
            savedPostsRepository = new SavedPostsRepository(new ApplicationDbContext());
        }

        // GET: Admin/AdminPosts
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            return View(postJobRepository.GetAllPostsOfUser(userid));
        }
        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostJob model)
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

                postJobRepository.CreatePost(model);
                postJobRepository.Save();                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

            // GET: 
        public ActionResult Edit(int? id)
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

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostJob model)
        {
            var post = postJobRepository.GetPostJobById(model.Id);

            string date = DateTime.Now.ToString("dd/MM/yyyy");

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
                post.CreationDate = Convert.ToDateTime(date);

                postJobRepository.EditPost(post);
                postJobRepository.Save();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {


            }

            return View();

        }


       // GET: 
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

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = postJobRepository.GetPostJobById(id);
            var SavedPost = savedPostsRepository.GetSavedsById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                postJobRepository.DeltePost(post);
                if (SavedPost != null)
                {
                    savedPostsRepository.DeleteSavedPost(SavedPost);
                }

                postJobRepository.Save();
                savedPostsRepository.SaveChange();
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
