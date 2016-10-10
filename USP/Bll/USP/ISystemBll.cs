using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISystemBll
    {
        DataGrid<UspController> getControllerGrid();
        DataGrid<UspMenuItem> getMenuGrid();
        DataGrid<UspPrivilege> getPrivilegeGrid();
        void UpdatePrivilege();
        void UpdateMenu();
        int AssignMenu();
        int AssignPrivilege();
    }
}