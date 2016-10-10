using System.Collections.Generic;
using System.Linq;
using USP.Models.POCO;
using USP.Dal;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Bll.Impl
{
    public class SysRoleBll : ISysRoleBll
    {
        private ISysRoleDal sysRoleDal;
        private ISysMenuDal sysMenuDal;
        private ISysPrivilegeDal sysPrivilegeDal;

        public SysRoleBll(ISysRoleDal sysRoleDal, ISysMenuDal sysMenuDal, ISysPrivilegeDal sysPrivilegeDal)
        {
            this.sysRoleDal = sysRoleDal;
            this.sysMenuDal = sysMenuDal;
            this.sysPrivilegeDal = sysPrivilegeDal;
        }

        public SysRole GetRoleByID(long id)
        {
            return sysRoleDal.GetRoleByID(id);
        }

        /// <summary>
        /// 获取用户的菜单和权限集合
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">指定角色</param>
        /// <returns></returns>
        public List<TreeNode> GetUserRoleMenuPrivilegeTree(long user, long? role)
        {
            var tree = new List<TreeNode>();

            //用户所有菜单、权限
            var userMenus = sysMenuDal.getMenuByOperator(user);
            var userPrivileges = sysPrivilegeDal.GetPrivilegeByOperator(user);


            //勾选角色的菜单、权限
            var roleMenus = new List<SysMenu>();
            var rolePrivileges = new List<SysPrivilege>();
            if (role != null)
            {
                var roleId = (long)role;
                roleMenus = sysMenuDal.getMenuByRole(roleId);
                rolePrivileges = sysPrivilegeDal.GetPrivilegeByRole(roleId);
            }
            GetUserRoleMenuPrivilegeTree(tree, userMenus, userPrivileges, roleMenus, rolePrivileges, 0);

            return tree;
        }

        /// <summary>
        /// 递归组织用户的菜单和权限集合
        /// </summary>
        private void GetUserRoleMenuPrivilegeTree(List<TreeNode> tree, List<SysMenu> userMenus, List<SysPrivilege> userPrivileges, List<SysMenu> roleMenus, List<SysPrivilege> rolePrivileges, long parentmenu)
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
                GetUserRoleMenuPrivilegeTree(menunode.children, userMenus, userPrivileges, roleMenus, rolePrivileges,
                    menu.ID);

                //菜单下的权限节点
                var menuPrivileges = userPrivileges.Where(x => x.Menu == menu.ID);
                foreach (var menuprivilege in menuPrivileges)
                {
                    var privilegenode = new TreeNode();
                    privilegenode.id = menuprivilege.ID;
                    privilegenode.text = menuprivilege.Name;
                    privilegenode.attributes = new { type = 2 };//1菜单节点  2权限节点
                    //节点的选中状态
                    if (rolePrivileges.Any(x => x.ID == menuprivilege.ID))
                    {
                        privilegenode.@checked = true;
                    }
                    else
                    {
                        privilegenode.@checked = false;
                    }
                    privilegenode.state = null;//节点展开或关闭状态 open  
                    menunode.children.Add(privilegenode);
                }
                if (menunode.children.Count > 0)
                {
                    menunode.state = "closed";
                    menunode.@checked = false;//只要有子节点，自身的选中状态就只能依赖所有层级子节点是否都选中
                }
                tree.Add(menunode);
            }
        }

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool CheckRoleName(string name, long corp)
        {
            return sysRoleDal.CheckRoleName(name, corp);
        }

        public bool AddRole(RoleAddEdit model)
        {
            return sysRoleDal.AddRole(model.Corp, model.Type, model.Name.Trim(), model.Remark, model.Menus, model.Privileges,
                model.Creator);
        }

        public bool EditRole(RoleAddEdit model)
        {
            return sysRoleDal.EditRole(model.ID, model.Name.Trim(), model.Remark, model.Menus, model.Privileges,
                model.Creator);
        }

        public DataGrid<SysRoleExt> GetSysRolePageByCorp(int page, int pagesize, long corp, string roleName)
        {
            var result = new DataGrid<SysRoleExt>();
            var strWhere = string.Empty;
            if (corp == 0)
            {
                strWhere = " ID>=0 and type=1 ";
            }
            else
            {
                strWhere = "ID>0 and Corp=" + corp;
            }
         
            if (!string.IsNullOrWhiteSpace(roleName) && Util.IsSafeSqlString(roleName.Trim()))
            {
                strWhere += " and Name like '%" + roleName + "%' ";
            }

            var roles = sysRoleDal.GetSysRolePage("*", strWhere, "ID ASC", page, pagesize);
            result.rows = roles;
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }

        public List<SysRole> GetSysRoleByOperator(long @operator)
        {
            return sysRoleDal.GetSysRoleByOperator(@operator).ToList();
        }

        public bool CancelOrActionRole(long role, long @operator)
        {
            return sysRoleDal.CancelOrActionRole(role, @operator);
        }
    }
}