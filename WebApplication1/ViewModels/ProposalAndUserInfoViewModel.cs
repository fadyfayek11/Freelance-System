using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ProposalAndUserInfoViewModel
    {
        public IEnumerable<ApplicationUser> UserData { get; set; }
        public IEnumerable<ProposalModels> ProposalData { get; set; }
    }
}