using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            flag = false;
            returnUrl = "";
            dateTime = DateTime.Now;
        }
        public bool flag { get; set; }
        public string message { get; set; }
        public string returnUrl { get; set; }
        public DateTime dateTime { get; set; }
        public object attachment { get; set; }
    }
}