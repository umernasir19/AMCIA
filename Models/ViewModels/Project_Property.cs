using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Project_Property
    {
        public int Project_ID { get; set; }
        [Required(ErrorMessage ="Required Field")]
        public string Project_Title { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Date)]
        public DateTime Project_Date { get; set; }
        public string Project_Details { get; set; }
        public string Project_FB_Link { get; set; }
        public string Project_Insta_Link { get; set; }
        public string Project_YT_Link { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}