using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Bll;
using USP.Common;
using USP.Filters;
using USP.Models.Entity;
using USP.Attributes;
namespace USP.Controllers
{
    [AuthorizeFilter]
    //[Menu(Name = "系统菜单", Icon = "glyphicon glyphicon-lock")]
    public class HomeController : Controller
    {
        //[MenuItem(Parent = "系统菜单", Name = "首页")]
        public ActionResult Index()
        {
            ViewData.Model = Session[Constants.USER_KEY];
            return View();
        }

        //[MenuItem(Parent = "系统菜单", Name = "首页")]
        public ActionResult Help()
        {
            return View();
        }

    }
}