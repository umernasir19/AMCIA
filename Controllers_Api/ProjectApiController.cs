using FL_ACME.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FL_ACME.Controllers_Api
{
    [RoutePrefix("api/Project")]
    public class ProjectApiController : BaseApiController
    {
        [Route("AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Project_PImages_VM _objmodelproperty)
        {
            if (ModelState.IsValid)
            {
                int flag=0;
                FL_PROJECT objdb = JsonConvert.DeserializeObject<FL_PROJECT>(JsonConvert.SerializeObject(_objmodelproperty.ProjectProperty));
                using (System.Data.Entity.DbContextTransaction trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (_objmodelproperty.ProjectProperty.Project_ID > 0)
                        {
                            //update
                            db.FL_PROJECT.Attach(objdb);
                            db.Entry(objdb).State = EntityState.Modified;
                            flag = db.SaveChanges();
                            for (int i = 0; i < _objmodelproperty.ProjectImageList.Count; i++)
                            {
                                //db = new FL_ACMEEntities();
                                _objmodelproperty.ProjectImageList[i].Project_ID = objdb.Project_ID;
                                FL_Project_Images objpimgdb = JsonConvert.DeserializeObject<FL_Project_Images>(JsonConvert.SerializeObject(_objmodelproperty.ProjectImageList[i]));

                                db.FL_Project_Images.Add(objpimgdb);
                                flag = db.SaveChanges();

                                //db.Dispose();
                            }
                            trans.Commit();
                        }
                        else
                        {
                            db.FL_PROJECT.Add(objdb);
                            flag = db.SaveChanges();


                            for (int i = 0; i < _objmodelproperty.ProjectImageList.Count; i++)
                            {
                                //db = new FL_ACMEEntities();
                                _objmodelproperty.ProjectImageList[i].Project_ID = objdb.Project_ID;
                                FL_Project_Images objpimgdb = JsonConvert.DeserializeObject<FL_Project_Images>(JsonConvert.SerializeObject(_objmodelproperty.ProjectImageList[i]));

                                db.FL_Project_Images.Add(objpimgdb);
                                flag = db.SaveChanges();
                                
                                //db.Dispose();
                            }
                            trans.Commit();

                        }

                        
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        flag = 0;
                        //Log, handle or absorbe I don't care ^_^
                    }
                }
               
                if (flag > 0)
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objmodelproperty };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseClass() { Status = false,Message="Save Failed", ResponseObject = _objmodelproperty };
                    return Ok(response);
                }

            }
            else
            {
                var response = new ResponseClass() { Status = true, Message = "Validation Failed", ResponseObject = _objmodelproperty };
                return Ok(response);
            }

        }

        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var returnobject = (db.FL_PROJECT.Where(p => p.Status == true).GroupJoin(db.FL_Project_Images,
                ProjectProperty => ProjectProperty.Project_ID,
                ProjectImageList => ProjectImageList.Project_ID, (ProjectProperty, ProjectImageList) => new { ProjectProperty, ProjectImageList })).ToList();
              
            if (returnobject.Count>0)
            {

                var response = new ResponseClass() { Status = true, ResponseObject = returnobject };
                return Ok(response);
            }
            else
            {
                var response = new ResponseClass() { Status = true, ResponseObject = returnobject };
                return Ok(response);
            }




        }

        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult Delete(Project_Property _objmasjidProperty)
        {
            int flag;
            //_objazkardBLL = new Azkar_BLL(_objazkarproperty);
            var masjiddbmodel = db.FL_PROJECT.Where(p => p.Project_ID == _objmasjidProperty.Project_ID).FirstOrDefault();
            masjiddbmodel.Status = false;

            db.FL_PROJECT.Attach(masjiddbmodel);
            db.Entry(masjiddbmodel).State = EntityState.Modified;
            flag = db.SaveChanges();
            if (flag > 0)
            {
                var response = new ResponseClass() { Status = true, ResponseObject = _objmasjidProperty };
                return Ok(response);
            }
            else
            {
                var response = new ResponseClass() { Status = true, ResponseObject = _objmasjidProperty };
                return Ok(response);
            }
        }

        [Route("GetAllForMobile")]
        [HttpGet]
        public IHttpActionResult GetAllForMobile()
        {
            var returnobject = (from proj in db.FL_PROJECT
                                where proj.Status == true
                                select new Project_Property
                                {
                                    Project_ID = proj.Project_ID,
                                    Project_Title = proj.Project_Title,
                                    Project_Details=proj.Project_Details,
                                    Project_FB_Link=proj.Project_FB_Link,
                                    Project_Insta_Link=proj.Project_Insta_Link,
                                    Project_YT_Link=proj.Project_YT_Link,
                                    //Status=Convert.ToBoolean(proj.Status),
                                    ProjectImages = db.FL_Project_Images.Where(p => p.Project_ID == proj.Project_ID).ToList()
                                }).ToList();

            var data = JsonConvert.SerializeObject(returnobject);
            var datatoreturn = JsonConvert.DeserializeObject<List<Project_Property>>(data);

            if (returnobject.Count > 0)
            {

                var response = new ResponseClass() { Status = true, ResponseObject = returnobject };
                return Ok(response);
            }
            else
            {
                var response = new ResponseClass() { Status = true, ResponseObject = returnobject };
                return Ok(response);
            }




        }
    }
}

