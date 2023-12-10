using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Article_Property
    {
        public int Aritcle_ID { get; set; }
        [Required(ErrorMessage ="Required Field")]
        public string Article_Title { get; set; }
        public string Article_Descr { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string Article_Notes { get; set; }
        public string Article_KeyPoints { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}