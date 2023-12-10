using FL_ACME.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FL_ACME.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Login()
        {

            return View(new LoginProperty());
        }

        [HttpPost]
        public JsonResult Login(LoginProperty Login)
        {
            var flag = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    var user = db.Users.Where(p => p.UserName == Login.email && p.Password==Login.password).FirstOrDefault();
                    if (user!=null)
                    {
                        return Json(new { Login = true, statuscode = 200, msg = "Login Successfull", url = "/Dashboard/Dashboard" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Login = flag, statuscode = 404, msg = msg, url = "/Account/Login" }, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    return Json(new { Login = flag, statuscode = 400, msg = ex.Message, url = "/Account/Login" }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                msg = "Validation Failed";
                return Json(new { Login = flag, statuscode = 400, msg = msg, url = "/Account/Login" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}