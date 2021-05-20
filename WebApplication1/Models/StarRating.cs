using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StarRating
    {
        [Key]
        public int IdOfRate { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User{ get; set; }
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual PostJob post { get; set; }
    }
}