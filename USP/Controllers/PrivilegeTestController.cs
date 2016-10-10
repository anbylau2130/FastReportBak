using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Filters;
namespace USP.Controllers
{
    public class PrivilegeTestController : SysPrivilegeController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}