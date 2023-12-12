using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Namaz_Property
    {
        public int Namaz_ID { get; set; }
        [Required(ErrorMessage ="Required Field")]
        public int Masjid_Id { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Time)]
        public TimeSpan Fajar_Time { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Time)]

        public TimeSpan Zuhar_Time { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Time)]

        public TimeSpan Asar_Time { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Time)]
        public TimeSpan Maghrib_Time { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Time)]

        public TimeSpan Isha_Time { get; set; }
        public TimeSpan Custom_Time { get; set; }
        public bool Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }

        public string Masjid_Title { get; set; }
        public List<Masjid_Property> MasjidList { get; set; }
    }
}