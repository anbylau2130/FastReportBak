using System.Collections.Generic;
using System.Linq;
using USP.Models.POCO;
using USP.Dal;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Bll.Impl
{
    public class SysPrivilegeBll : ISysPrivilegeBll
    {
        private ISysMenuDal sysMenuDal;
        private ISysPrivilegeDal sysPrivilegeDal;
        public SysPrivilegeBll(ISysMenuDal sysMenuDal, ISysPrivilegeDal sysPrivilegeDal)
        {
            this.sysMenuDal = sysMenuDal;
            this.sysPrivilegeDal = sysPrivilegeDal;
        }

        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="checkedmenu"></param>
        /// <returns></returns>
        public List<TreeNode> GetMenuTree(long parent, long? checkedmenu)
        {
            var tree = new List<TreeNode>();
            var menus = sysMenuDal.GetAllMenus().Where(x => x.ID != 0 ).ToList();
            GetMenuTree(tree, menus, parent, checkedmenu);
            return tree;
        }

        private void GetMenuTree(List<TreeNode> tree, List<SysMenu> menus, long parent, long? checkedmenu)
        {
            var submenus = menus.Where(x => x.Parent == parent && x.ID != parent);
            foreach (var menu in submenus)
            {
                var menunode = new TreeNode();
                menunode.id = menu.ID;
                menunode.text = menu.Name;

                if (menu.ID == checkedmenu)
                {
                    menunode.@checked = true;
                }

                menunode.children = new List<TreeNode>();

                //递归加载子节点
                GetMenuTree(menunode.children, menus, menu.ID, checkedmenu);

                if (menunode.children.Count > 0)
                {
                    menunode.state = "closed";
                }
                tree.Add(menunode);
            }
        }

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool CheckPrivilegeName(string name, long menu)
        {
            return sysPrivilegeDal.CheckPrivilegeName(name, menu);
        }

        public SysPrivilege GetPrivilegeByID(long id)
        {
            return sysPrivilegeDal.GetPrivilegeByID(id);
        }

        public SysPrivilege AddPrivilege(SysPrivilege model)
        {
            return sysPrivilegeDal.AddPrivilege(model);
        }

        public SysPrivilege EditPrivilege(SysPrivilege model)
        {
            return sysPrivilegeDal.EditPrivilege(model);
        }

        /// <summary>
        /// 获取权限分页数据
        /// </summary>
        public DataGrid<SysPrivilegeExt> SearchSysPrivilegePage(int page, int pagesize, long? menu)
        {
            var result = new DataGrid<SysPrivilegeExt>();

            var strWhere = "ID>0 ";
            if (menu != null)
            {
                strWhere += " and Menu=" + menu;
            }

            var roles = sysPrivilegeDal.GetSysPrivilegePage("*", strWhere, "ID ASC", page, pagesize);

            var menus = sysMenuDal.GetAllMenus();
            foreach (var role in roles)
            {
                var firstOrDefault = menus.FirstOrDefault(x => x.ID == role.Menu);
                if (firstOrDefault != null)
                    role.MenuName = firstOrDefault.Name;
                else
                    role.MenuName = "";
            }

            result.rows = roles;
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }

        /// <summary>
        /// 标记删除权限
        /// </summary>
        /// <param name="id">权限id</param>
        /// <param name="op">操作人</param>
        /// <returns></returns>
        public bool DeletePrivilege(long id, long op)
        {
            return sysPrivilegeDal.DeletePrivilege(id, op);
        }

        /// <summary>
        /// 激活权限
        /// </summary>
        /// <param name="id">权限id</param>
        /// <param name="op">操作人</param>
        /// <returns></returns>
        public bool ActionPrivilege(long id, long op)
        {
            return sysPrivilegeDal.ActionPrivilege(id, op);
        }
    }
}