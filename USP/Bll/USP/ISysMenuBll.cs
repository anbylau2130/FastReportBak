using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysMenuBll
    {
        void addMenu(string name, string icon);
        void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url);

        List<Models.Entity.SysMenu> getMenuGrid(string name);
        SysMenu GetModel(long @id);
        AjaxResult IsExistMenuName(long id, long parent, string name);
        AjaxResult Add(Models.Entity.SysMenu model);
        AjaxResult Edit(Models.Entity.SysMenu model);
        AjaxResult Audit(Models.Entity.SysMenu model);
        AjaxResult Cancel(long id, long operater);
        //List<USP.Models.POCO.TreeNode> QueryTreeList(string id);
    }
}