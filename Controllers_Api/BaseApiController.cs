using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FL_ACME.Controllers_Api
{
    public class BaseApiController : ApiController
    {
        protected FL_ACMEEntities db;

        public BaseApiController()
        {
            db = new FL_ACMEEntities();
        }
    }
}
