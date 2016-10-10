using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class UserMenu
    {
        public SysMenu SysMenu { get; set; }
        public List<UserMenu> Children { get; set; }

        public UserMenu(SysMenu sysMenu, List<UserMenu> children)
        {
            SysMenu = sysMenu;
            Children = children;
        }
    }
}