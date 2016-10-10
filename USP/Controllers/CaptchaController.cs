using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Service;

namespace USP.Controllers
{
    public class CaptchaController : Controller
    {

        ICaptchaService capthcaService;

        public CaptchaController(ICaptchaService capthcaService)
        {
            this.capthcaService = capthcaService;
        }

        public void  Index()
        {
            capthcaService.RenderImage(this.ControllerContext);
        }
    }
}