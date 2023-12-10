using FL_ACME.Controllers_Api;
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
    public class EventController : BaseController
    {
        // GET: Events
        public ActionResult Events()
        {
            List<Event_Property> objAzkarList = new List<Event_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Event/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objAzkarList = JsonConvert.DeserializeObject<List<Event_Property>>(data);


                return View(objAzkarList);
            }
            else

            {
                objAzkarList = new List<Event_Property>();
                return View(objAzkarList);
            }
        }

        public ActionResult AddEvent(int? id)
        {
            Event_Property objeventprop = new Event_Property();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/Event/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objeventprop = JsonConvert.DeserializeObject<List<Event_Property>>(data).Where(p => p.Event_Id == id).FirstOrDefault();

                }
                return PartialView("_AddEvent", objeventprop);
            }
            else
            {
                return PartialView("_AddEvent", objeventprop);
            }

        }

        [System.Web.Http.HttpPost]
        public JsonResult AddUpdate(Event_Property objevent, FormCollection forms)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files[0];

                    var imagsave = Utility.SaveFiles(postedFile);
                    if (imagsave.Status)
                    {
                        objevent.ImagePath = imagsave.Message;
                    }
                }
                objevent.DateCreated = DateTime.UtcNow;
                objevent.Status = true;
                // objmasjid.ImageFile = null;
                var v = Utility.InteractToAPI<ResponseClass>("api/Event/AddUpdate", true, objevent);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Event/Events" }, JsonRequestBehavior.AllowGet);
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
                var model = new Event_Property();
                model.Event_Id = id;

              
                var v = Utility.InteractToAPI<ResponseClass>("api/Event/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Event/Events" }, JsonRequestBehavior.AllowGet);
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