using FL_ACME.Models.ViewModels;
using FL_ACME.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FL_ACME.Controllers
{
    public class SpecialDateController : BaseController
    {
        // GET: SpecialDate
        public ActionResult SpecialDate()
        {
            List<SpecialDate_Property> objmasjidlist = new List<SpecialDate_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/SpecialDate/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmasjidlist = JsonConvert.DeserializeObject<List<SpecialDate_Property>>(data);


                return View(objmasjidlist);
            }
            else

            {
                objmasjidlist = new List<SpecialDate_Property>();
                return View(objmasjidlist);
            }
        }

        public ActionResult AddSpecialDate(int? id)
        {
            SpecialDate_Property objmsjidprop = new SpecialDate_Property();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/SpecialDate/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objmsjidprop = JsonConvert.DeserializeObject<List<SpecialDate_Property>>(data).Where(p => p.S_ID == id).FirstOrDefault();
                                 

                   
                }
                else
                {

                }

            }
            return PartialView("_AddSpecialDate", objmsjidprop);
        }

        [System.Web.Http.HttpPost]
        public JsonResult AddUpdate(SpecialDate_Property objmasjid, FormCollection forms)
        {
            try
            {
                //if (Request.Files.Count > 0)
                //{
                //    HttpPostedFileBase postedFile = Request.Files[0];

                //    var imagsave = Utility.SaveFiles(postedFile);
                //    if (imagsave.Status)
                //    {
                //        objmasjid.ImagePath = imagsave.Message;
                //    }
                //}
                objmasjid.DateCreated = DateTime.UtcNow;
                objmasjid.Status = true;
                objmasjid.CreatedBy = 0;
                // objmasjid.ImageFile = null;
                var v = Utility.InteractToAPI<ResponseClass>("api/SpecialDate/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/SpecialDate/SpecialDate" }, JsonRequestBehavior.AllowGet);
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
                var model = new SpecialDate_Property();
                model.S_ID = id;
                var v = Utility.InteractToAPI<ResponseClass>("api/SpecialDate/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/SpecialDate/SpecialDate" }, JsonRequestBehavior.AllowGet);
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