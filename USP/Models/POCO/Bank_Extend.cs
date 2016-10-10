using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class Bank_Extend:SysBank
    {
        public long? RowNo { get; set; }
        public long? RowCnt { get; set; }
    }
}