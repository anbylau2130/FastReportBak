using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class UspMenu
    {
        public UspMenu(string icon,string name)
        {
            Icon = icon;
            Name = name;
        }
        public string Icon { get; set; }
        public string Name { get; set; }
    }
}