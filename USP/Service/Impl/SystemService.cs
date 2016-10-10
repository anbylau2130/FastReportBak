using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using USP.Models.POCO;
using USP.Attributes;
using USP.Utility;
namespace USP.Service.Impl
{
    public class SystemService : ISystemService
    {
        public List<UspController> getControllers()
        {
            var controllers = from controller in AssemblyUtil.GetAllSubTypesEnum(typeof(Controller))
                              from action in controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                              where !action.IsSpecialName
                              select new UspController(controller.FullName, GetAreaName(controller.FullName), GetControllerName(controller.FullName), action.Name, string.Join(",", action.GetParameters().Select(p => p.Name).ToArray()));
            return controllers.ToList();
        }
        public List<UspMenu> getMenus()
        {
            var menus = from controller in AssemblyUtil.GetAllSubTypesEnum(typeof(Controller))
                        from attribute in controller.GetCustomAttributes(typeof(Menu), false)
                        where attribute != null
                        select new UspMenu(((Menu)attribute).Icon, ((Menu)attribute).Name);
            return menus.ToList(); ;
        }

        public List<UspMenuItem> getMenuItems()
        {
            var menus = from controller in AssemblyUtil.GetAllSubTypesEnum(typeof(Controller))
                        from action in controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                        where !action.IsSpecialName
                        from attribute in action.GetCustomAttributes(typeof(MenuItem), false)
                        where attribute != null
                        select new UspMenuItem(((MenuItem)attribute).Icon, ((MenuItem)attribute).Name, ((MenuItem)attribute).Parent, controller.FullName, GetAreaName(controller.FullName), GetControllerName(controller.FullName), action.Name, string.Join(",", action.GetParameters().Select(p => p.Name).ToArray()));
            return menus.ToList(); ;
        }

        public List<UspPrivilege> getPrivileges()
        {
            var privileges = from controller in AssemblyUtil.GetAllSubTypesEnum(typeof(Controller))
                             from action in controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                             where !action.IsSpecialName
                             from attribute in action.GetCustomAttributes(typeof(Privilege), false)
                             where attribute != null
                             select new UspPrivilege(((Privilege)attribute).Menu, ((Privilege)attribute).Parent, ((Privilege)attribute).Name, controller.FullName, GetAreaName(controller.FullName), GetControllerName(controller.FullName), action.Name, string.Join(",", action.GetParameters().Select(p => p.Name).ToArray()));
            return privileges.ToList(); ;
        }

        private string GetAreaName(string fullName)
        {
            Regex areaRegex = new Regex("Areas.(\\w*).Controllers", RegexOptions.IgnoreCase);
            Match match = areaRegex.Match(fullName);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }
            else
            {
                return "";
            }
        }

        private string GetControllerName(string fullName)
        {
            Regex areaRegex = new Regex("Controllers.(\\w*)Controller", RegexOptions.IgnoreCase);
            Match match = areaRegex.Match(fullName);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}