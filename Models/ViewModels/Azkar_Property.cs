using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Azkar_Property
    {
        public int Azkar_ID { get; set; }
        [Required(ErrorMessage = "Please Enter Azkar Name")]
        [DataType(DataType.Text)]
        public string Azkar_Name { get; set; }
        public string Azkar_Arabic { get; set; }
        public string Azkar_Eng { get; set; }
        public string Azkar_Urdu { get; set; }
        [Required(ErrorMessage = "Please Enter Azkar Count")]

        public int Azkar_Count { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}