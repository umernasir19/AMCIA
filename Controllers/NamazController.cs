using FL_ACME.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FL_ACME.Controllers
{
    public class NamazController : BaseController
    {
        // GET: Namaz
        public ActionResult Namaz()
        {
            return View();
        }


        public ActionResult AddNamaz(int? id)
        {
            Masjid_Property objmsjidprop = new Masjid_Property();
            if (id > 0)
            {
                objmsjidprop.MAsjid_ID = 1;
                objmsjidprop.Masjid_Location = "";
                objmsjidprop.Masjid_Title = "Fajar namaz";
                objmsjidprop.Masjid_Lat = Convert.ToDecimal(1.2);
                objmsjidprop.Masjid_Lon = Convert.ToDecimal(1.2);
                objmsjidprop.Masjid_Descr = "Description Of Masjid";
                objmsjidprop.Masjid_Location = "New York";
                objmsjidprop.Rating = Convert.ToDecimal(4);

            }
            return PartialView("_AddNamaz", objmsjidprop);
        }
    }
}