using FL_ACME.Controllers;
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
    [RoutePrefix("api/Azkar")]
    public class AzkarApiController : BaseApiController
    {
        
            
            [Route("AddUpdate")]
            [HttpPost]
            public IHttpActionResult AddUpdate(Azkar_Property _objazkarproperty)
            {
            if (ModelState.IsValid)
            {
                int flag;
                FL_AZKAR objazkardb = JsonConvert.DeserializeObject<FL_AZKAR>(JsonConvert.SerializeObject(_objazkarproperty));
                if (_objazkarproperty.Azkar_ID > 0)
                {
                    //update
                    db.FL_AZKAR.Attach(objazkardb);
                    db.Entry(objazkardb).State = EntityState.Modified;
                    flag= db.SaveChanges();
                }
                else
                {
                    db.FL_AZKAR.Add(objazkardb);
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
                var response= new ResponseClass() { Status = true,Message="Validation Failed", ResponseObject = _objazkarproperty };
                return Ok(response);
            }

        }

            [Route("GetAll")]
            [HttpGet]
            public IHttpActionResult GetAll()
            {
                var azkar = db.FL_AZKAR.Where(p => p.Status == true).ToList();
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
            public IHttpActionResult Delete(Azkar_Property _objazkarproperty)
            {
            int flag;
            //_objazkardBLL = new Azkar_BLL(_objazkarproperty);
            var azkardbmodel = db.FL_AZKAR.Where(p => p.Azkar_ID == _objazkarproperty.Azkar_ID).FirstOrDefault();
            azkardbmodel.Status = false;

            db.FL_AZKAR.Attach(azkardbmodel);
            db.Entry(azkardbmodel).State = EntityState.Modified;
            flag = db.SaveChanges();
            if (flag>0)
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
