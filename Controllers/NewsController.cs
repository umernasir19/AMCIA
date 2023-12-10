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
    public class NewsController : BaseController
    {
        // GET: News
        public ActionResult News()
        {
            List<TimeLine_Property> objmasjidlist = new List<TimeLine_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Timeline/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmasjidlist = JsonConvert.DeserializeObject<List<TimeLine_Property>>(data);


                return View(objmasjidlist);
            }
            else

            {
                objmasjidlist = new List<TimeLine_Property>();
                return View(objmasjidlist);
            }
        }

        public ActionResult AddNews(int? id)
        {
            TimeLine_Property objmsjidprop = new TimeLine_Property();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/TimeLine/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objmsjidprop = JsonConvert.DeserializeObject<List<TimeLine_Property>>(data).Where(p => p.Timeline_Id == id).FirstOrDefault();
                }
                else
                {

                }
               
            }
            return PartialView("_AddNEWS", objmsjidprop);
        }

        [System.Web.Http.HttpPost]
        public JsonResult AddUpdate(TimeLine_Property objmasjid, FormCollection forms)
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
                var v = Utility.InteractToAPI<ResponseClass>("api/TimeLine/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/News/News"}, JsonRequestBehavior.AllowGet);
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
                var model = new TimeLine_Property();
                model.Timeline_Id = id;
                var v = Utility.InteractToAPI<ResponseClass>("api/Timeline/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/News/News" }, JsonRequestBehavior.AllowGet);
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