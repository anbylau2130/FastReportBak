using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;
namespace USP.Bll.Impl
{
    public class SysMenuTemplateBll : ISysMenuTemplateBll
    {
        ISysMenuTemplateDal sysMenuTemplateDal;
        ISysMenuDal sysMenuDal;
        public SysMenuTemplateBll(ISysMenuTemplateDal sysMenuTemplateDal, ISysMenuDal sysMenuDal)
        {
            this.sysMenuTemplateDal = sysMenuTemplateDal;
            this.sysMenuDal = sysMenuDal;
        }

        public AjaxResult Add(long corpType, long creator, string menu)
        {
            AjaxResult result = new AjaxResult();
            if (sysMenuTemplateDal.Add(corpType, creator, menu) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，添加成功！";
            }
            else
            {
                result.flag = false;
                result.message = "添加失败！";
            }
            return result;
        }

        public AjaxResult Edit(long corpType, long creator, string menu)
        {
            AjaxResult result = new AjaxResult();
            if (sysMenuTemplateDal.Edit(corpType, creator, menu) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，编辑成功！";
            }
            else
            {
                result.flag = false;
                result.message = "编辑失败！";
            }
            return result;
        }


        /// <summary>
        /// 获取用户的菜单和权限集合
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">指定角色</param>
        /// <returns></returns>
        public List<TreeNode> GetUserRoleMenuPrivilegeTree(long corpType)
        {
            var tree = new List<TreeNode>();
            var corpTypeMenus = new List<SysMenu>();
            if (corpType == 0)
            {
                corpTypeMenus = sysMenuDal.GetAllMenus().Where(x => x.ID != 0 && x.Canceler == null).ToList();
            }
            else
            {
                corpTypeMenus = sysMenuDal.GetAllMenus().Where(x => x.ID != 0 && x.Canceler == null && x.Name != "系统数据").ToList();
            }
            //勾选角色的菜单
            var roleMenus = new List<SysMenu>();

            var list = sysMenuTemplateDal.GetCorpList(corpType).Select(x => x.Menu).ToList();
            roleMenus = new USP.Context.USPEntities().SysMenu.Where(x => list.Contains(x.ID)).ToList();
            GetCorpTypeMenuTree(tree, corpTypeMenus, roleMenus, 0);

            return tree;
        }

        /// <summary>
        /// 递归组织用户的菜单和权限集合
        /// </summary>
        private void GetCorpTypeMenuTree(List<TreeNode> tree, List<SysMenu> userMenus, List<SysMenu> roleMenus, long parentmenu)
        {
            var menus = userMenus.Where(x => x.Parent == parentmenu && x.ID != parentmenu);
            foreach (var menu in menus)
            {
                var menunode = new TreeNode();
                menunode.id = menu.ID;
                menunode.text = menu.Name;
                menunode.attributes = new { type = 1 };//1菜单节点  2权限节点
                //节点的选中状态
                if (roleMenus.Any(x => x.ID == menu.ID))
                {
                    menunode.@checked = true;
                }
                else
                {
                    menunode.@checked = false;
                }

                menunode.children = new List<TreeNode>();

                //递归加载子节点
                GetCorpTypeMenuTree(menunode.children, userMenus, roleMenus, menu.ID);


                if (menunode.children.Count > 0)
                {
                    menunode.state = "closed";
                    menunode.@checked = false;//只要有子节点，自身的选中状态就只能依赖所有层级子节点是否都选中
                }
                tree.Add(menunode);
            }
        }

        public AjaxResult Audit(long corpType, long creator, string menu)
        {
            AjaxResult result = new AjaxResult();
            if (sysMenuTemplateDal.Audit(corpType, creator, menu) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，审核成功！";
            }
            else {
                result.flag = false;
                result.message = "审核失败！";

            }
            return result;
        }

        public SysMenuTemplate GetModel(long id)
        {
            return sysMenuTemplateDal.GetModel(id);
        }
        public List<SysMenuTemplate> GetCorpList(long corp)
        {
            return sysMenuTemplateDal.GetCorpList(corp);
        }
        public List<SysMenuTemplate> getSysMenuTemplateList()
        {
            return sysMenuTemplateDal.getSysMenuTemplateList();
        }

        public List<UP_GetMenuTemplate_Result> getSysMenuTempGrid(long id)
        {
            return sysMenuTemplateDal.getSysMenuTempGrid(id);
        }
    }
}