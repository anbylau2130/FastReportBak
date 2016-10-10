using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace USP.Common
{
    public static class ExtensionController
    {
        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">行为</param>
        /// <returns>JsonReuslt</returns>
        public static JsonResult ToJson(this Controller controller,object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }

        /// <summary>
        /// 返回JsonResult.24         
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        public static JsonResult ToJson(this Controller controller, object data, JsonRequestBehavior behavior, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult 
        /// </summary>      
        /// <param name="data">数据</param>
        /// <param name="format">数据格式</param>
        /// <returns>Json</returns>
        public static JsonResult ToJson(this Controller controller, object data, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }


        public static JsonResult ToJson(this Controller controller, object data)
        {

            return new CustomJsonResult
            {
                Data = data,
                FormateStr = "yyyy-MM-dd HH:mm:ss"
            };
        }
    }
}
