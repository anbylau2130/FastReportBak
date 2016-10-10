using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Attributes
{
    public class MenuItem : Attribute
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public string Icon { get; set; }
        public string ErrorMessage { get; set; }
    }
}