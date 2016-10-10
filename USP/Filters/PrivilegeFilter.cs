using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Common;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Filters
{
    public class PrivilegeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session[Constants.USER_KEY] == null)
            {
                filterContext.Result = new RedirectResult("/User/Login", true);
                return;
            }
            var user = HttpContext.Current.Session[Constants.USER_KEY] as User;
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/User/Login", true);
                return;
            }

            //检测是否跳过验证
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoFilter), true);
            if (attrs.Length == 1)
            {
                var noFilter = attrs[0] as NoFilter;
                if (noFilter != null && noFilter.Flag)
                {
                    //跳过验证
                    return;
                }
            }
            string @class = filterContext.Controller.GetType().FullName;
            string area = Util.GetAreaName(@class);
            string controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            var url = "/" + area + "/" + controller + "/" + action;

            //测试时可以注释以下验证
            if (!ckeckPrivilege(user, url, @class, area, controller, action))
            {
                filterContext.Result = new RedirectResult("/Error/Http401", true);
                return;
            }
        }

        /// <summary>
        /// 权限判断，仅通过url判断，其他参数暂时保留
        /// </summary>
        /// <returns></returns>
        private bool ckeckPrivilege(User user, string url, string @class, string area, string controller, string action)
        {
            if (checkMenu(url, @class, area, controller, action, user.Menus))
            {
                return true;
            }

            //return user.Privileges.Any(
            //    x => x.Clazz == @class && x.Area == area && x.Controller == controller && x.Method == action);
            return user.Privileges.Any(x => string.Equals(x.Url.Replace("~", ""), url, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 权限判断，仅通过url判断，其他参数暂时保留
        /// </summary>
        private bool checkMenu(string url, string @class, string area, string controller, string action, List<UserMenu> Menus)
        {
            var flag = false;
            foreach (var menu in Menus)
            {
                var sysMenu = menu.SysMenu;
                //if (sysMenu.Clazz == @class && sysMenu.Area == area && sysMenu.Controller == controller &&
                //    sysMenu.Method == action)
                if (string.Equals(sysMenu.Url.Replace("~", ""), url, StringComparison.CurrentCultureIgnoreCase))
                {
                    flag = true;
                    break;
                }
                flag = checkMenu(url, @class, area, controller, action, menu.Children);
                if (flag) break;
            }
            return flag;
        }
    }
}