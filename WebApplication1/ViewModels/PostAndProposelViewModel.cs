using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class PostAndProposelViewModel
    {
        public IEnumerable<PostJob> PostJob { get; set; }
        public IEnumerable<ProposalModels> Proposals { get; set; }
    }
}