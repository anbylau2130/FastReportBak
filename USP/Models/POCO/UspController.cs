using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class UspController
    {
        public UspController(string @class, string area, string controller, string action, string @params)
        {
            Class = @class;
            Area = area;
            Controller = controller;
            Action = action;
            Params = @params;
        }
        public string Class { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }  
    }
}