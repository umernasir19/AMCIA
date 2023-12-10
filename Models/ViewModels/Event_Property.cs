using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Event_Property
    {
		public int Event_Id { get; set; }
        [Required(ErrorMessage ="Required")]
		public string Event_Name { get; set; }
		[Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
		public DateTime Event_Date_Eng { get; set; }
		[Required(ErrorMessage = "Required")]
		[DataType(DataType.Date)]
		public DateTime Event_Date_Urdu { get; set; }
		[MaxLength(50,ErrorMessage ="Only 50  chars allowed")]
		public string Event_Location { get; set; }
		public bool Status { get; set; }
		public DateTime DateCreated { get; set; }
		public int CreatedBy { get; set; }

		public string ImagePath { get; set; }

	}
}