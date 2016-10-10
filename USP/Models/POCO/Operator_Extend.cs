using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class Operator_Extend:SysOperator
    {
        public long? RowNo { get; set; }
        public long? RowCnt { get; set; }

        public  string Supplier { get; set; }

        public string SupplierName { get; set; }
    }
}