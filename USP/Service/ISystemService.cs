using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.POCO;

namespace USP.Service
{
    public interface ISystemService
    {
        List<UspController> getControllers();
        List<UspMenu> getMenus();
        List<UspMenuItem> getMenuItems();
        List<UspPrivilege> getPrivileges();
    }
}