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
    [RoutePrefix("api/Namaz")]
    public class NamazApiController : BaseApiController
    {
        [Route("AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Namaz_Property _objazkarproperty)
        {
            if (ModelState.IsValid)
            {
                int flag;
                FL_NAMAZ objazkardb = JsonConvert.DeserializeObject<FL_NAMAZ>(JsonConvert.SerializeObject(_objazkarproperty));
                if (_objazkarproperty.Namaz_ID > 0)
                {
                    //update
                    db.FL_NAMAZ.Attach(objazkardb);
                    db.Entry(objazkardb).State = EntityState.Modified;
                    flag = db.SaveChanges();
                }
                else
                {
                    db.FL_NAMAZ.Add(objazkardb);
                    flag = db.SaveChanges();
                }
                if (flag > 0)
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objazkarproperty };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objazkarproperty };
                    return Ok(response);
                }

            }
            else
            {
                var response = new ResponseClass() { Status = true, Message = "Validation Failed", ResponseObject = _objazkarproperty };
                return Ok(response);
            }

        }

        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var azkar = (from nmz in db.FL_NAMAZ join msj in db.FL_MASJID on
                        nmz.Masjid_Id equals msj.MAsjid_ID where nmz.Status==true select new {
                            nmz.Namaz_ID,
                            nmz.Masjid_Id,
                            nmz.Fajar_Time,
                            nmz.Zuhar_Time,
                            nmz.Asar_Time,
                            nmz.Maghrib_Time,
                            nmz.Isha_Time,
                            nmz.Custom_Time,
                            nmz.DateCreated,
                            nmz.CreatedBy,
                            msj.Masjid_Title,
                            msj.MAsjid_ID
                        }).ToList();
                //db.FL_NAMAZ.Where(p => p.Status == true).ToList();
            if (azkar.Count > 0)
            {

                var response = new ResponseClass() { Status = true, ResponseObject = azkar };
                return Ok(response);
            }
            else
            {
                var response = new ResponseClass() { Status = true, ResponseObject = azkar };
                return Ok(response);
            }




        }

        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult Delete(Namaz_Property _objazkarproperty)
        {
            int flag;
            //_objazkardBLL = new Azkar_BLL(_objazkarproperty);
            var azkardbmodel = db.FL_NAMAZ.Where(p => p.Namaz_ID == _objazkarproperty.Namaz_ID).FirstOrDefault();
            azkardbmodel.Status = false;

            db.FL_NAMAZ.Attach(azkardbmodel);
            db.Entry(azkardbmodel).State = EntityState.Modified;
            flag = db.SaveChanges();
            if (flag > 0)
            {
                var response = new ResponseClass() { Status = true, ResponseObject = _objazkarproperty };
                return Ok(response);
            }
            else
            {
                var response = new ResponseClass() { Status = true, ResponseObject = _objazkarproperty };
                return Ok(response);
            }
        }
    }
}
