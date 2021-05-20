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
    [Authorize(Roles =SD.FreelancerUser)]
    public class AcceptedProposalController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AcceptedProposalController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: FreeLancer/AcceptedProposal
        public ActionResult Index()
        {            
            return View(_db.Proposals.Where(p=>p.IsAccepted == true));
        }
        
       
    }
}
