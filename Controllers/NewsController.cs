using FL_ACME.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FL_ACME.Controllers
{
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult News()
        {
            return View();
        }

        public ActionResult AddNews(int? id)
        {
            Masjid_Property objflmsjd = new Masjid_Property();
            if (id > 0)
            {
                objflmsjd.MAsjid_ID = 1;
                objflmsjd.Masjid_Location = "English meaning of this azkar is bjfbdak";
                objflmsjd.Masjid_Title = "SubhanALLAH";
                objflmsjd.Masjid_Lat = Convert.ToDecimal(1.2);
                objflmsjd.Masjid_Lon = Convert.ToDecimal(1.2);
                objflmsjd.Masjid_Descr = "Urdu Meaning";
                objflmsjd.Masjid_Location = "New York";
                objflmsjd.Rating = Convert.ToDecimal(4);

            }
            return PartialView("_AddNEWS", objflmsjd);
        }
    }
}