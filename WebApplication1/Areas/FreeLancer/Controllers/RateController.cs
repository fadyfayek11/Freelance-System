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
    public class RateController : Controller
    {
        private readonly IPostJobRepository postJobRepository;
        private readonly ApplicationDbContext _db;
        public RateController()
        {
            postJobRepository = new PostJobRepository(new ApplicationDbContext());
            _db = new ApplicationDbContext();
        }
       
        public async Task<ActionResult> RateJob(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            var post = postJobRepository.GetPostJobById(id);
            var userID = User.Identity.GetUserId();
            var IsRatingBefore = await _db.Stars.FirstOrDefaultAsync(r => r.PostId == post.Id && r.UserId == userID);
            if (IsRatingBefore != null)
            {
                ViewBag.MyRate = IsRatingBefore.Rating;
            }
         
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RateJob(PostJob model)
        {
            var post = postJobRepository.GetPostJobById(model.Id);
            var userID = User.Identity.GetUserId();
            if (post == null)
            {
                return HttpNotFound();
            }
            StarRating rating = new StarRating();
            PostJob postfromDb = new PostJob();

            var IsRatingBefore = await _db.Stars.FirstOrDefaultAsync(r => r.PostId == model.Id && r.UserId == userID);          
            if (IsRatingBefore != null)
            {
                ViewBag.AlreadyRatedMessage= "You have already Rate this job";               
            }
            else
            {

                rating.PostId = model.Id;
                rating.UserId = userID;
                rating.Rating = model.Rate;



                postfromDb = _db.PostJobs.Find(model.Id);
                postfromDb.Rate = (postfromDb.Rate + rating.Rating) / 2;

                _db.Stars.Add(rating);
                _db.Entry(postfromDb).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RedirectToAction("Index","Posts",new { area = ""});
            }          
           
            return View(post);
        }
    }
}