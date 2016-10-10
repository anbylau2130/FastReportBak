using System.Collections.Generic;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysRoleBll
    {
        /// <summary>
        /// 获取用户的菜单和权限集合
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">指定角色</param>
        /// <returns></returns>
        List<TreeNode> GetUserRoleMenuPrivilegeTree(long user, long? role);

        SysRole GetRoleByID(long id);

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool CheckRoleName(string name, long corp);

        bool AddRole(RoleAddEdit model);

        bool EditRole(RoleAddEdit model);

        /// <summary>
        /// 根据公司获取角色列表
        /// </summary>
        DataGrid<SysRoleExt> GetSysRolePageByCorp(int page, int pagesize, long corp, string roleName);

        List<SysRole> GetSysRoleByOperator(long @operator);

        bool CancelOrActionRole(long role, long @operator);
    }
}