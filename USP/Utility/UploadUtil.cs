using System;
using System.Web;
using System.IO;
namespace USP.Utility
{
    public class UploadUtil
    {
        public static bool UploadFile(HttpPostedFileBase file, string uploadPath, string fileName)
        {
            if (file != null)
            {   
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(uploadPath + fileName);

                return true;
            }
            else
            {
                return false;
            };
        }
    }
}