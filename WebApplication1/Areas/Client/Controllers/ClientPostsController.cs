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

namespace WebApplication1.Areas.Client.Controllers
{
    [Authorize(Roles = SD.ClientUser)]
    public class ClientPostsController : Controller
    {
      
        private readonly IPostJobRepository postJobRepository;
        private readonly ISavedPostsRepository savedPostsRepository;
        public ClientPostsController()
        {
            postJobRepository = new PostJobRepository(new ApplicationDbContext());
            savedPostsRepository = new SavedPostsRepository(new ApplicationDbContext());
        }

        // GET: Client/ClientPosts
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();            
            return View(postJobRepository.GetAllPostsOfUser(userid));
        }

        // GET: Client/ClientPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }           
            var post =  postJobRepository.GetPostJobById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Client/ClientPosts/Edit/5
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

        // GET: Client/ClientPosts/Delete/5
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
        // POST: Client/ClientPosts/Delete/5
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
