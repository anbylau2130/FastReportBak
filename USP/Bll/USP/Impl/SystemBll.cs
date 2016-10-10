using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using USP.Common;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using USP.Utility;

namespace USP.Bll.Impl
{
    public class SystemBll : ISystemBll
    {
        ISystemService systemService;
        ISysMenuService SysMenuService;
        ISysPrivilegeService sysPrivilegeService;
        public SystemBll(ISystemService systemService, ISysMenuService SysMenuService, ISysPrivilegeService sysPrivilegeService)
        {
            this.systemService = systemService;
            this.SysMenuService = SysMenuService;
            this.sysPrivilegeService = sysPrivilegeService;
        }
        public DataGrid<UspController> getControllerGrid()
        {
            DataGrid<UspController> result = new DataGrid<UspController>();
            result.rows = systemService.getControllers().ToList();
            result.total = result.rows.Count;
            return result;
        }
        public DataGrid<UspMenuItem> getMenuGrid()
        {
            UpdateMenu();
            DataGrid<UspMenuItem> result = new DataGrid<UspMenuItem>();
            result.rows = systemService.getMenuItems();
            result.total = result.rows.Count;
            return result;
        }
        public DataGrid<UspPrivilege> getPrivilegeGrid()
        {
            UpdatePrivilege();
            DataGrid<UspPrivilege> result = new DataGrid<UspPrivilege>();
            result.rows = systemService.getPrivileges();
            result.total = result.rows.Count;
            return result;
        }

        public void UpdatePrivilege()
        {
            foreach (UspPrivilege uspPrivilege in systemService.getPrivileges())
            {
                sysPrivilegeService.addPrivilege(uspPrivilege.Menu, uspPrivilege.Parent, uspPrivilege.Name, uspPrivilege.ControllerClass, uspPrivilege.ControllerArea, uspPrivilege.ControllerName, uspPrivilege.ControllerAction, uspPrivilege.ActionParams, uspPrivilege.Url);
            }
        }
        public void UpdateMenu()
        {
            //add menu
            foreach (UspMenu uspMenu in systemService.getMenus())
            {
                SysMenuService.addMenu(uspMenu.Name, uspMenu.Icon);
            }
            //add menuitem
            foreach (UspMenuItem uspMenuItem in (from menu in systemService.getMenuItems() where menu.Parent != menu.Name select menu))
            {
                SysMenuService.addMenuItem(uspMenuItem.Parent, uspMenuItem.Name, uspMenuItem.Icon, uspMenuItem.ControllerClass, uspMenuItem.ControllerArea, uspMenuItem.ControllerName, uspMenuItem.ControllerAction, uspMenuItem.ActionParams, uspMenuItem.Url);
            }
        }

        public int AssignMenu()
        {
           return SysMenuService.AssignMenu();
        }

        public int AssignPrivilege()
        {
            return sysPrivilegeService.AssignPrivilege();
        }
    }
}