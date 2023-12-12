using FL_ACME.Models.ViewModels;
using FL_ACME.Utilities;
using Newtonsoft.Json;
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
            List<Namaz_Property> objmasjidlist = new List<Namaz_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Namaz/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmasjidlist = JsonConvert.DeserializeObject<List<Namaz_Property>>(data);


                return View(objmasjidlist);
            }
            else

            {
                objmasjidlist = new List<Namaz_Property>();
                return View(objmasjidlist);
            }
        }


        public ActionResult AddNamaz(int? id)
        {
            Namaz_Property objmsjidprop = new Namaz_Property();
            
            if (id > 0)
            {

                var namaz = Utility.InteractToAPI<ResponseClass>("api/Namaz/GetAll", false, null);
                if (namaz != null)
                {
                    var data = JsonConvert.SerializeObject(namaz.ResponseObject);
                    objmsjidprop = JsonConvert.DeserializeObject<List<Namaz_Property>>(data).Where(p => p.Namaz_ID == id).FirstOrDefault();

                }
               
            }
            objmsjidprop.MasjidList = new List<Masjid_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Masjid/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmsjidprop.MasjidList = JsonConvert.DeserializeObject<List<Masjid_Property>>(data);
            }
            return PartialView("_AddNamaz", objmsjidprop);
        }
        [HttpPost]
        public JsonResult AddUpdate(Namaz_Property objmasjid, FormCollection forms)
        {
            try
            {
               
                objmasjid.DateCreated = DateTime.UtcNow;
                objmasjid.Status = true;
                // objmasjid.ImageFile = null;
                var v = Utility.InteractToAPI<ResponseClass>("api/Namaz/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Namaz/Namaz" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { data = v.ResponseObject, flag = v.Status, statuscode = 500, msg = v.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Login = false, statuscode = 400, msg = ex.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(int id)
        {
            try
            {
                var model = new Namaz_Property();
                model.Namaz_ID = id;

               
                var v = Utility.InteractToAPI<ResponseClass>("api/Namaz/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Namaz/Namaz" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { data = v.ResponseObject, flag = v.Status, statuscode = 500, msg = v.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", flag = false, statuscode = 400, msg = ex.InnerException.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}