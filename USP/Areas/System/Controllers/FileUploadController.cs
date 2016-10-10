using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Common;
using USP.Context;
using USP.Controllers;
using USP.Utility;

namespace USP.Areas.System.Controllers
{
    public class FileUploadController : Controller
    {

        private string Directory
        {
            get
            {
                return Server.MapPath("~/Upload/");
            }
        }
        [HttpPost]
        public ActionResult Upload()
        {
            HttpPostedFileBase fileUpload= Request.Files[0];
            ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
            ControllerContext.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            ControllerContext.HttpContext.Response.Charset = "UTF-8";
          
            var fileName = Guid.NewGuid() + Path.GetExtension(fileUpload.FileName);
            if (UploadUtil.UploadFile(fileUpload, Directory, fileName))
            {
                return Json(new { Msg = "文件上传成功！", IsSuccess = true,FileName= "/Upload/"+fileName });
            }
            return Json(new { Msg = "文件上传失败！", IsSuccess = false });
        }
        
    }
}