using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FL_ACME.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected FL_ACMEEntities db;
        public ActionResult Index()
        {
            return View();
        }

        public BaseController()
        {
            db = new FL_ACMEEntities();
        }


    }

}