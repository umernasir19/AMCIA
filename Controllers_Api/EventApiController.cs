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
    [RoutePrefix("api/Event")]
    public class EventApiController : BaseApiController
    {
        [Route("AddUpdate")]
        [HttpPost]
        public IHttpActionResult AddUpdate(Event_Property _objeventproperty)
        {
            if (ModelState.IsValid)
            {
                int flag;
                FL_EVENTS objazkardb = JsonConvert.DeserializeObject<FL_EVENTS>(JsonConvert.SerializeObject(_objeventproperty));
                if (_objeventproperty.Event_Id > 0)
                {
                    //update
                    db.FL_EVENTS.Attach(objazkardb);
                    db.Entry(objazkardb).State = EntityState.Modified;
                    flag = db.SaveChanges();
                }
                else
                {
                    db.FL_EVENTS.Add(objazkardb);
                    flag = db.SaveChanges();
                }
                if (flag > 0)
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objeventproperty };
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseClass() { Status = true, ResponseObject = _objeventproperty };
                    return Ok(response);
                }

            }
            else
            {
                var response = new ResponseClass() { Status = true, Message = "Validation Failed", ResponseObject = _objeventproperty };
                return Ok(response);
            }

        }

        [Route("GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var azkar = db.FL_EVENTS.Where(p => p.Status == true).ToList();
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
        public IHttpActionResult Delete(Event_Property _objazkarproperty)
        {
            int flag;
            //_objazkardBLL = new Azkar_BLL(_objazkarproperty);
            var azkardbmodel = db.FL_EVENTS.Where(p => p.Event_Id == _objazkarproperty.Event_Id).FirstOrDefault();
            azkardbmodel.Status = false;

            db.FL_EVENTS.Attach(azkardbmodel);
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
