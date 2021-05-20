using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PostJob
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ForeignUser")]
        public string UserId { get; set; }

        public virtual ApplicationUser ForeignUser { get; set; }

        [Required]
        [Display(Name = "Owner Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage =" Price should be greater than ${1}")]
        [Display(Name = "Job Budget")]
        public double JobBudget { get; set; }

        [Required]
        [DataType(DataType.Date)]       
        [Display(Name =" Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Discription { get; set; }


        public bool IsStillAvilavble { get; set; }

        [Display(Name = " Number Of Submitted")]
        public int NumberOfSubmitted { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter a value in range 1:5")]
        public int Rate { get; set; }

        public bool IsAvilavbleAtWall { get; set; }


        public bool? IsNotificationOfPostsRequestSeen { get; set; } 


    }
}