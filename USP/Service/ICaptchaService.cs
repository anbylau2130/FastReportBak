using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace USP.Service
{
    public interface ICaptchaService
    {
        void RenderImage(ControllerContext context);
    }
}