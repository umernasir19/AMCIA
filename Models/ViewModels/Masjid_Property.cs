using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FL_ACME.Models.ViewModels
{
    public class Masjid_Property
    {
        public int MAsjid_ID { get; set; }
        [Required(ErrorMessage = "Enter Field")]
        public string Masjid_Title { get; set; }
        public string Masjid_Descr { get; set; }
        public string Masjid_Location { get; set; }
        public decimal Masjid_Lat { get; set; }
        public decimal Masjid_Lon { get; set; }
        public bool Status { get; set; }
        public decimal Rating { get; set; }

        public string ImagePath { get; set; }

        //[NonSerialized]

        //private HttpPostedFileWrapper _ImageFile;

        //public HttpPostedFileWrapper ImageFile {
        //    get { return _ImageFile; }
        //    set { _ImageFile = value; }
        //}

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
    }
}