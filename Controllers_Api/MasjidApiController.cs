using FL_ACME.Models.ViewModels;
using FL_ACME.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FL_ACME.Controllers_Api
{
    [RoutePrefix("api/Masjid")]
    public class MasjidApiController : BaseApiController
    {
        [Route("AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Masjid_Property _objmodelproperty)
        {
            if (ModelState.IsValid)
            {

                if (HttpContext.Current.Request.Files.Count>0)
                {
                    HttpPostedFile postedFile =HttpContext.Current.Request.Files[0];

                    //var imagsave = Utility.SaveFiles(postedFile);
                    //if (imagsave.Status)
                    //{
                    //    _objmodelproperty.ImagePath = imagsave.Message;
                    //}
                }
                int flag;
                FL_MASJID objdb = JsonConvert.DeserializeObject<FL_MASJID>(JsonConvert.SerializeObject(_objmodelproperty));
                if (_objmodelproperty.MAsjid_ID > 0)
                {
                    //update
                    db.FL_MASJID.Attach(objdb);
                    db.Entry(objdb).State = EntityState.Modified;
                    flag = db.SaveChanges();
                }
                else
                {
                    db.FL_MASJID.Add(objdb);
                    flag = db.SaveChanges();
                }
                if (flag > 0)
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objmodelproperty };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objmodelproperty };
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
            var azkar = db.FL_MASJID.Where(p => p.Status == true).ToList();
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
        public IHttpActionResult Delete(Masjid_Property _objmasjidProperty)
        {
            int flag;
            //_objazkardBLL = new Azkar_BLL(_objazkarproperty);
            var masjiddbmodel = db.FL_MASJID.Where(p => p.MAsjid_ID == _objmasjidProperty.MAsjid_ID).FirstOrDefault();
            masjiddbmodel.Status = false;

            db.FL_MASJID.Attach(masjiddbmodel);
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
    }
}
