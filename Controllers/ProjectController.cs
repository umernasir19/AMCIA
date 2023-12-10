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
    public class ProjectController : BaseController
    {
        // GET: Project
        public ActionResult Project()
        {
            List<Project_PImages_VM> objmasjidlist = new List<Project_PImages_VM>();
            var v = Utility.InteractToAPI<ResponseClass>("api/Project/GetAll", false, null);
            if (v != null)
            {
                var data = JsonConvert.SerializeObject(v.ResponseObject);
                objmasjidlist = JsonConvert.DeserializeObject<List<Project_PImages_VM>>(data);


                return View(objmasjidlist);
            }
            else

            {
                objmasjidlist = new List<Project_PImages_VM>();
                return View(objmasjidlist);
            }
        }
        public ActionResult AddProjects(int? id)
        {
            Project_PImages_VM objflmsjd = new Project_PImages_VM();
            objflmsjd.ProjectImageList = new List<Project_Images_Property>();
            if (id > 0)
            {
                var v = Utility.InteractToAPI<ResponseClass>("api/Project/GetAll", false, null);
                if (v != null)
                {
                    var data = JsonConvert.SerializeObject(v.ResponseObject);
                    objflmsjd = JsonConvert.DeserializeObject<List<Project_PImages_VM>>(data).Where(p => p.ProjectProperty.Project_ID == id ).FirstOrDefault();

                }
                else
                {

                }

            }
            return PartialView("_AddProjects", objflmsjd);
        }


        [HttpPost]
        public JsonResult AddUpdate(Project_PImages_VM objmasjid, FormCollection forms)
        {
            try
            {
                objmasjid.ProjectImageList = new List<Project_Images_Property>();
                if (Request.Files.Count > 0)
                {
                   
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase postedFile = Request.Files[i];

                        var imagsave = Utility.SaveFiles(postedFile);
                        if (imagsave.Status)
                        {
                            Project_Images_Property pimg = new Project_Images_Property();
                            pimg.ImagePath = imagsave.Message;
                            objmasjid.ProjectImageList.Add(pimg);
                            // objmasjid.ProjectImageList.ImagePath = imagsave.Message;
                        }
                    }
                }
                objmasjid.ProjectProperty.DateCreated = DateTime.UtcNow;
                objmasjid.ProjectProperty.Status = true;
                // objmasjid.ImageFile = null;
                var v = Utility.InteractToAPI<ResponseClass>("api/Project/AddUpdate", true, objmasjid);
                if (v != null)
                {
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Project/Project" }, JsonRequestBehavior.AllowGet);
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
                var model = new Project_Property();
                model.Project_ID = id;
                var v = Utility.InteractToAPI<ResponseClass>("api/Project/Delete", true, model);
                if (v != null)
                {
                    // var data = v;// JsonConvert.SerializeObject(v.ResponseObject);
                    return Json(new { data = v, flag = v.Status, statuscode = 200, msg = v.Message, url = "/Project/Project" }, JsonRequestBehavior.AllowGet);
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