using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace USP.Controllers
{
    public class AuthorizeTestController : AuthorizeController
    {
        // GET: AuthorizeTest
        public ActionResult Index()
        {
            return View();
        }
    }
}