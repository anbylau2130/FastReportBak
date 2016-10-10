using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace USP.Models.POCO
{
    public class DataGrid<T>
    {
        public long total { get; set; }
        public List<T> rows { get; set; }
    }
}