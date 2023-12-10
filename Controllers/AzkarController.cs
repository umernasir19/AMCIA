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
    public class AzkarController : BaseController
    {
        // GET: Azkar
        AzkarApiController objazkarapi;
        public ActionResult Azkar()

        {
            List<Azkar_Property> objAzkarList = new List<Azkar_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Azkar/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objAzkarList = JsonConvert.DeserializeObject<List<Azkar_Property>>(data);


                return View(objAzkarList);
            }
            else

            {
                objAzkarList = new List<Azkar_Property>();
                return View(objAzkarList);
            }
        }
        [HttpPost]
        public JsonResult AddUpdate(FormCollection objform)
        {
            try
            {
                var model = new Azkar_Property();
                UpdateModel<Azkar_Property>(model);
                if (ModelState.IsValid)
                {
                    objazkarapi = new AzkarApiController();
                     
                    model.DateCreated = DateTime.UtcNow;
                    model.CreatedBy = 1;
                    model.Status = true;
                    var v = Utility.InteractToAPI<ResponseClass>("api/Azkar/AddUpdate", true, model);
                    if (v != null)
                    {
                        // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                        return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Azkar/Azkar" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { data = v.ResponseObject, flag = v.Status, statuscode = 500, msg = v.Message, url = "#" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = model, flag = false, statuscode = 500, msg = "Validation Failed", url = "#" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = "", flag = false, statuscode = 400, msg = ex.InnerException.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AddAzkar(int? id)
        {
            Azkar_Property objazkar = new Azkar_Property();
            if (id > 0)
            {

                var v = Utility.InteractToAPI<ResponseClass>("api/Azkar/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objazkar = JsonConvert.DeserializeObject<List<Azkar_Property>>(data).Where(p => p.Azkar_ID == id).FirstOrDefault();

                }
                return PartialView("_AddAzkar", objazkar);
            }
            else
            {
                return PartialView("_AddAzkar", objazkar);
            }



        }
        public JsonResult Delete(int id)
        {
            try
            {
                var model = new Azkar_Property();
                model.Azkar_ID = id;

                objazkarapi = new AzkarApiController();
                var v = Utility.InteractToAPI<ResponseClass>("api/Azkar/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Azkar/Azkar" }, JsonRequestBehavior.AllowGet);
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
