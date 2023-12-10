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
    public class ArticleController : BaseController
    {
        // GET: Article
        public ActionResult Article()
        {
            List<Article_Property> objAzkarList = new List<Article_Property>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Article/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objAzkarList = JsonConvert.DeserializeObject<List<Article_Property>>(data);


                return View(objAzkarList);
            }
            else

            {
                objAzkarList = new List<Article_Property>();
                return View(objAzkarList);
            }
        }

        [HttpPost]
        public JsonResult AddUpdate(Article_Property objmasjid, FormCollection forms)
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
                var v = Utility.InteractToAPI<ResponseClass>("api/Article/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Article/Article" }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { data = v.ResponseObject, flag = v.Status, statuscode = 500, msg = v.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Login = false, statuscode = 400, msg = ex.Message, url = "#" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddArticle(int? id)
        {
            Article_Property objflmsjd = new Article_Property();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/Article/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objflmsjd = JsonConvert.DeserializeObject<List<Article_Property>>(data).Where(p => p.Aritcle_ID == id).FirstOrDefault();
                }
                else
                {

                }

            }
            return PartialView("_AddArticle", objflmsjd);
        }

        public JsonResult Delete(int id)
        {
            try
            {
                var model = new Article_Property();
                model.Aritcle_ID = id;
                var v = Utility.InteractToAPI<ResponseClass>("api/article/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Article/Article" }, JsonRequestBehavior.AllowGet);
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