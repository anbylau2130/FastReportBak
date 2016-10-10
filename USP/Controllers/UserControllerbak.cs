using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using USP.Bll;
using USP.Common;
using USP.Filters;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
namespace USP.Controllers
{
    public class UserController : Controller
    {
        ISysOperatorBll SysOperatorBll;
        public UserController(ISysOperatorBll SysOperatorBll)
        {
            this.SysOperatorBll = SysOperatorBll;
        }

        public ActionResult Login()
        {
            Session.Abandon();
            Login login = new Login();
            login.Name = "admin";
            login.Password = "123456";
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (!login.Captcha.Equals(HttpContext.Session[Constants.CAPTCHA]))
                {
                    ModelState.AddModelError("Captcha", "验证码输入错误");
                    return View("Login", login);
                }

                var result = SysOperatorBll.Login(login, HttpContext);

                if (result.flag == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //ModelState.AddModelError("Name", "用户名或密码错误");
                    //ModelState.AddModelError("Password", "密码或用户名错误");

                    ModelState.AddModelError("Password", result.message);

                    return View("Login", login);
                }
            }
            return View("Login", login);
        }

        //public ActionResult CheckSso()
        //{
        //    return Json(SysOperatorBll.CheckSso(HttpContext), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Logout()
        {
            Session.Abandon();
            return new HttpUnauthorizedResult();
        }

        public ActionResult Registe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registe(Object obj)
        {
            return View();
        }


        [AuthorizeFilter]
        public ActionResult AuthorizeTest()
        {
            return View();
        }

        [PrivilegeFilter]
        public ActionResult PrivilegeTest()
        {
            return View();
        }
    }
}