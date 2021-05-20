using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Saved
    {
        [Key]
        public int IdOfSavedPost { get; set; }
        public bool? IsMarked { get; set; }
        public string IdOfTheUser { get; set; }
        [ForeignKey("IdOfTheUser")]
        public virtual ApplicationUser users { get; set; }
        public int IdOfThePost { get; set; }
        [ForeignKey("IdOfThePost")]
        public virtual IEnumerable<PostJob> SavePostes { get; set; }
    }
}