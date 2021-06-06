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
    [Authorize(Roles =SD.FreelancerUser)]
    public class AcceptedProposalController : Controller
    {

        private readonly ApplicationDbContext _db;
        public PostAndProposelViewModel ProVM { get; set; }
        public AcceptedProposalController()
        {
            _db = new ApplicationDbContext();
            ProVM = new PostAndProposelViewModel()
            {
                PostJob = _db.PostJobs,
                Proposals = _db.Proposals
            };
        }
        // GET: FreeLancer/AcceptedProposal
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            ProVM.Proposals = _db.Proposals.Where(p => p.FreeLancerId == userID && p.IsAccepted == true);
            var m = from p in _db.PostJobs
                    join u in ProVM.Proposals
                    on p.Id equals u.PostId
                    select p;
            ProVM.PostJob = m.ToList();
            return View(ProVM);
        }
        
       
    }
}
