using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProposalModels
    {
        [Key]
        public int IdOfProposal { get; set; }

        [Required]
        [Display(Name = "The Proposal")]
        public string TheProposal { get; set; }

        public string ClientId { get; set; }

        public string FreeLancerId { get; set; }
        [ForeignKey("FreeLancerId")]
       
        public virtual ApplicationUser user { get; set; }

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual PostJob post { get; set; }

        public bool? IsAccepted { get; set; }

    }
}