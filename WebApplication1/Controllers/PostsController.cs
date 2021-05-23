using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Utility;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPostJobRepository postJobRepository;
        private readonly ISavedPostsRepository savedPostsRepository;
        public PostsController()
        {
            _db = new ApplicationDbContext();
            postJobRepository = new PostJobRepository(new ApplicationDbContext());
            savedPostsRepository = new SavedPostsRepository(new ApplicationDbContext());
        }
        // GET: Posts
        [AllowAnonymous]
        public ActionResult Index(string option, string search)
        {            

            if (search != null)
            {
                if (option == "Jop Type")
                {
                    return View(_db.PostJobs.Where(p => p.JobType.Contains(search) && p.IsAvilavbleAtWall == true || search == null).ToList());
                }
                else if (option == "Date")
                {
                    try
                    {
                        DateTime SearchDate = Convert.ToDateTime(search);
                        return View(_db.PostJobs.Where(p => p.CreationDate == SearchDate && p.IsAvilavbleAtWall == true || search == null).ToList());

                    }
                    catch (Exception)
                    {

                        
                    }
                    return View(_db.PostJobs.Where(p => p.IsAvilavbleAtWall == true));
                }
                else 
                {
                    return View(_db.PostJobs.Where(p => p.UserName.Contains(search) && p.IsAvilavbleAtWall == true || search == null).ToList());
                }
            }
            else
            {
                return View(_db.PostJobs.Where(p => p.IsAvilavbleAtWall == true));
            }        

        }       

        
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostJob model)
        {
            var post = postJobRepository.GetPostJobById(model.Id);

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

                postJobRepository.EditPost(post);
                postJobRepository.Save();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {


            }

            return View();

        }

        // GET: Posts/Delete/5
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

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var post = postJobRepository.GetPostJobById(id);
            var SavedPost =savedPostsRepository.GetSavedsById(id);
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
                await savedPostsRepository.SaveChangeAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        
        [AllowAnonymous]
        public ActionResult Details(int? id)
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

    }
}
