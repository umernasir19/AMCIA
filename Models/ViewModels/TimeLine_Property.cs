using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class TimeLine_Property
    {
        public int Timeline_Id { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [MaxLength(50, ErrorMessage = "Only 50  chars allowed")]
        public string TimeLine_Name { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [MaxLength(50, ErrorMessage = "Only 50  chars allowed")]
        public string Timeline_Short { get; set; }
        [MaxLength(50, ErrorMessage = "Only 50  chars allowed")]
        public string TimeLine_Urdu { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}