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

namespace WebApplication1.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostsController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: Posts
        [AllowAnonymous]
        public ActionResult Index(string option, string search)
        {
            var userid = User.Identity.GetUserId();
            ViewBag.ProposalRequest = _db.Proposals.Where(p => p.ClientId == userid && p.IsAccepted == null).Count();

            ViewBag.CountRequest = _db.PostJobs.Where(p => p.IsStillAvilavble == false).Count();
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
       



        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostJob model)
        {
           
            var userid = User.Identity.GetUserId();
            try
            {
                model.IsStillAvilavble = true;
                model.IsAvilavbleAtWall = true;
                model.NumberOfSubmitted = 0;
                model.Rate = 1;

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

        // GET: Posts/Delete/5
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

        // POST: Posts/Delete/5
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
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
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

    }
}
