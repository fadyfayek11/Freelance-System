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

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    public class AdminReceivedProposalController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProposalAndUserInfoViewModel PropAndUserVM { get; set; }
        public PostAndProposelViewModel ProVM { get; set; }

        public AdminReceivedProposalController()
        {
            _db = new ApplicationDbContext();
            ProVM = new PostAndProposelViewModel()
            {
                PostJob = _db.PostJobs,
                Proposals = _db.Proposals
            };
            PropAndUserVM = new ProposalAndUserInfoViewModel()
            {
                UserData = _db.Users,
                ProposalData = _db.Proposals
            };
        }
        // GET: Admin/AdminReceivedProposal
        public ActionResult Index()
        {
            ViewBag.CountRequest = _db.PostJobs.Where(p => p.IsStillAvilavble == false).Count();

            var myId = User.Identity.GetUserId();

            ProVM.Proposals = _db.Proposals.Where(p => p.ClientId == myId && p.IsAccepted == null);
            var m = from p in _db.PostJobs
                    join u in ProVM.Proposals
                    on p.Id equals u.PostId
                    select p;
            ProVM.PostJob = m.ToList();
            return View(ProVM);
        }

        // GET: Admin/AdminReceivedProposal/Approve/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            PropAndUserVM.ProposalData = _db.Proposals.Where(p => p.IdOfProposal == id);
            var m = from p in _db.Users
                    join u in PropAndUserVM.ProposalData
                    on p.Id equals u.FreeLancerId
                    select p;
            PropAndUserVM.UserData = m.ToList();
            return View(PropAndUserVM);
        }

        //Post: Admin/AdminReceivedProposal/Approve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(int id)
        {
            var proposalDb = await _db.Proposals.FirstOrDefaultAsync(p => p.IdOfProposal == id);

            if (proposalDb == null)
            {
                return HttpNotFound();
            }

            try
            {
                proposalDb.IsAccepted = true;

                var postid = proposalDb.PostId;
                var AllProposelWithTheId = await _db.Proposals.FirstOrDefaultAsync(p => p.PostId == postid && p.IsAccepted == null);
                var PostOfTheID = await _db.PostJobs.FirstOrDefaultAsync(p => p.Id == postid);
                PostOfTheID.IsAvilavbleAtWall = false;
                _db.Proposals.Remove(AllProposelWithTheId);

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Reject(int? id)
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

        // POST: Admin/ReceivdProposals/Reject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reject(int id)
        {
            try
            {
                var proposal = await _db.Proposals.FirstOrDefaultAsync(p => p.IdOfProposal == id);
                if (proposal == null)
                {
                    return HttpNotFound();
                }
                _db.Proposals.Remove(proposal);

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
