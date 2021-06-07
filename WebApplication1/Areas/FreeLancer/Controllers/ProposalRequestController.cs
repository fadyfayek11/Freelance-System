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
using WebApplication1.ViewModels;

namespace WebApplication1.Areas.FreeLancer.Controllers
{
    [Authorize(Roles = SD.FreelancerUser)]
    public class ProposalRequestController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostAndProposelViewModel ProVM { get; set; }
        public ProposalRequestController()
        {
            _db = new ApplicationDbContext();
            ProVM = new PostAndProposelViewModel()
            {
                PostJob = _db.PostJobs,
                Proposals = _db.Proposals
            };
        }

        public  ActionResult Index()
        {            
            var userID = User.Identity.GetUserId();

            ProVM.Proposals = _db.Proposals.Where(p => p.FreeLancerId == userID && p.IsAccepted == null);
            var m = from p in _db.PostJobs
                    join u in ProVM.Proposals
                    on p.Id equals u.PostId
                    select p;
            ProVM.PostJob = m.ToList();
            return View(ProVM);
        }

        

        // GET: FreeLancer/ProposalRequest/Create/post Id
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: FreeLancer/ProposalRequest/Create
        [HttpPost]
        public async Task<ActionResult> Create(int id, ProposalModels myProp)
        {
            
            var post = await _db.PostJobs.FirstOrDefaultAsync(p=>p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.NumberOfSubmitted++;
            var userID = User.Identity.GetUserId();
            myProp.FreeLancerId = userID;
            myProp.PostId = id;
            myProp.ClientId = post.UserId;
            if (ModelState.IsValid)
            {
                bool existprop = await _db.Proposals.FirstOrDefaultAsync(p=>p.PostId == id) != null;
                if (existprop)
                {
                    ViewBag.ExistProp = "You have already send a Proposal Before";
                }
                else
                {
                    _db.Proposals.Add(myProp);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Posts", new { area = "" });
                }
               
            }
            return View();            
        }

        // GET: FreeLancer/ProposalRequest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var proposal = await _db.Proposals.FirstOrDefaultAsync(p=>p.IdOfProposal == id);
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        // POST: FreeLancer/ProposalRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProposalModels proposal)
        {
            var propfromdb = await _db.Proposals.FirstOrDefaultAsync(p=>p.IdOfProposal == id);
            var postid = propfromdb.PostId; 
            if(propfromdb == null)
            {
                return HttpNotFound();
            }     
           if (ModelState.IsValid)
           {
                propfromdb.TheProposal = proposal.TheProposal;
                propfromdb.PostId = postid;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
           }               
           return View();            
        }

        // GET: FreeLancer/ProposalRequest/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var proposal = await _db.Proposals.FirstOrDefaultAsync(p => p.IdOfProposal == id);
            if (proposal == null)
            {
                return HttpNotFound();
            }
            return View(proposal);
        }

        // POST: FreeLancer/ProposalRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var proposal = await _db.Proposals.FirstOrDefaultAsync(p => p.IdOfProposal == id);
                var postid = proposal.PostId;
                var post = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == postid);
                if (proposal == null)
                {
                    return HttpNotFound();
                }
                _db.Proposals.Remove(proposal);
                post.NumberOfSubmitted--;
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
