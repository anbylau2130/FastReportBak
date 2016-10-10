using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Attributes
{
    public class Privilege : Attribute
    {

        public string Menu { get; set; }

        public string Parent{ get; set; }

        public string Name { get; set; }

        public string ErrorMessage { get; set; }
    }
}