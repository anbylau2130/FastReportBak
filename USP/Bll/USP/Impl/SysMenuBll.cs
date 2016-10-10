using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;


namespace USP.Bll.Impl
{
    public class SysMenuBll : ISysMenuBll
    {
        USP.Dal.ISysMenuDal sysMenuDal;
        public SysMenuBll(USP.Dal.ISysMenuDal sysMenuDal)
        {
            this.sysMenuDal = sysMenuDal;
        }

        public void addMenu(string name, string icon)
        {
            sysMenuDal.addMenu(name, icon);
        }

        public void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url)
        {
            sysMenuDal.addMenuItem(parent, name, icon, @class, area, controller, action, parameter, url);
        }

        public List<Models.Entity.SysMenu> getMenuGrid(string name)
        {
            return sysMenuDal.getMenuGrid(name);
        }

        public SysMenu GetModel(long id)
        {
            return sysMenuDal.GetModel(id);
        }
        public AjaxResult IsExistMenuName(long id, long parent, string name)
        {
            AjaxResult ajaxResult = new AjaxResult();


            if (sysMenuDal.IsExistMenuName(id, parent, name))
            {
                ajaxResult.flag = true;
                ajaxResult.message = "菜单名已存在！";
            }
            else
            {
                ajaxResult.flag = false;
                ajaxResult.message = "";
            }

            return ajaxResult;
        }
        public AjaxResult Add(Models.Entity.SysMenu model)
        {
            AjaxResult ajaxResult = new AjaxResult();

            var result = sysMenuDal.Add(model);

            String[] res = result.Split(new char[] { '|' });

            ajaxResult.flag = Convert.ToBoolean(res[0]);
            ajaxResult.message = res[1];

            return ajaxResult;
        }

        public AjaxResult Edit(Models.Entity.SysMenu model)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var result = sysMenuDal.Edit(model);
            String[] res = result.Split(new char[] { '|' });

            ajaxResult.flag = Convert.ToBoolean(res[0]);
            ajaxResult.message = res[1];

            return ajaxResult;
        }

        public AjaxResult Audit(Models.Entity.SysMenu model)
        {
            AjaxResult result = new AjaxResult();
            if (sysMenuDal.Audit(model) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，审核通过！";
            }
            else
            {
                result.flag = false;
                result.message = "审核失败！";
            }
            return result;
        }

        public AjaxResult Cancel(long id, long operater)
        {
            AjaxResult ajaxResult = new AjaxResult();

            var result = sysMenuDal.Cancel(id, operater);

            String[] res = result.Split(new char[] { '|' });

            ajaxResult.flag = Convert.ToBoolean(res[0]);
            ajaxResult.message = res[1];

            return ajaxResult;
        }

        //public List<TreeNode> QueryTreeList(string id)
        //{
        //    return sysMenuDal.QueryTreeList(id);
        //}
    }
}