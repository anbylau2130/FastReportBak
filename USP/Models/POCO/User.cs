using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class User
    {
        public SysOperator SysOperator { get; set; }
        public SysCorp SysCorp { get; set; }
        public List<UserMenu> Menus { get; set; }
        public List<SysPrivilege> Privileges { get; set; }
        //public T_BD_SUPPLIER Supplier { get; set; }
    }
}