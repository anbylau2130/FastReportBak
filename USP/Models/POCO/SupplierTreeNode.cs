using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class SupplierTreeNode
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public object Attach { get; set; }
    }
}