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
    public class MasjidController : Controller
    {
        // GET: Masjid
        
        public ActionResult Masjid()
        {

            List<Masjid_Property> objmasjidlist = new List<Masjid_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Masjid/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmasjidlist = JsonConvert.DeserializeObject<List<Masjid_Property>>(data);


                return View(objmasjidlist);
            }
            else

            {
                objmasjidlist = new List<Masjid_Property>();
                return View(objmasjidlist);
            }
            
        }

        public ActionResult AddMasjid(int? id)
        {
            Masjid_Property objmsjidprop = new Masjid_Property();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/Masjid/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objmsjidprop = JsonConvert.DeserializeObject<List<Masjid_Property>>(data).Where(p=>p.MAsjid_ID==id).FirstOrDefault();
                }
                else
                {

                }
               

            }
            return PartialView("_AddMasjid", objmsjidprop);

        }

        [System.Web.Http.HttpPost]
        public JsonResult AddUpdate(Masjid_Property objmasjid,FormCollection forms)
        {
                try
                {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files[0];
                    
                    var imagsave = Utility.SaveFiles(postedFile);
                    if (imagsave.Status)
                    {
                        objmasjid.ImagePath = imagsave.Message;
                    }
                }
                objmasjid.DateCreated = DateTime.UtcNow;
                objmasjid.Status = true;
                // objmasjid.ImageFile = null;
                var v = Utility.InteractToAPI<ResponseClass>("api/Masjid/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Masjid/Masjid" }, JsonRequestBehavior.AllowGet);
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
                var model = new Masjid_Property();
                model.MAsjid_ID = id;
                var v = Utility.InteractToAPI<ResponseClass>("api/Masjid/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Masjid/Masjid" }, JsonRequestBehavior.AllowGet);
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