using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace USP.Common
{
    public class JObjectModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var stream = controllerContext.RequestContext.HttpContext.Request.InputStream;
            stream.Seek(0, SeekOrigin.Begin);
            string json = new StreamReader(stream).ReadToEnd();
            if (JsonChecker.IsJson(json))
            {
                return JsonConvert.DeserializeObject<dynamic>(json);
            }
            return JsonConvert.DeserializeObject<dynamic>(JsonChecker.Querystring2Json(json));
        }
    }
}
