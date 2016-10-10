using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class LoginResult
    {
        public List<SysOperator> SysOperators { get; set; }
        public List<SysMenu> SysMenus { get; set; }
        public List<SysPrivilege> SysPrivileges { get; set; }
    }
}