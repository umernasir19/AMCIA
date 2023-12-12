using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class SpecialDate_Property
    {
        public int S_ID { get; set; }
        [Required(ErrorMessage ="Enter Event Name")]
        [DataType(DataType.Text)]

        public string  Event_Name { get; set; }
        [Required(ErrorMessage = "Enter Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime English_date { get; set; }

        public  string Urdu_Date { get; set; }

        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}