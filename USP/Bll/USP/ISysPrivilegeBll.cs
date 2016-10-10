using System.Collections.Generic;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysPrivilegeBll
    {
        /// <summary>
        /// 获取菜单树列表
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="checkedmenu"></param>
        /// <returns></returns>
        List<TreeNode> GetMenuTree(long parent, long? checkedmenu);

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool CheckPrivilegeName(string name, long menu);

        SysPrivilege GetPrivilegeByID(long id);

        SysPrivilege AddPrivilege(SysPrivilege model);

        SysPrivilege EditPrivilege(SysPrivilege model);

        /// <summary>
        /// 获取分页权限数据
        /// </summary>
        DataGrid<SysPrivilegeExt> SearchSysPrivilegePage(int page, int pagesize, long? menuId);

        /// <summary>
        /// 标记删除权限
        /// </summary>
        /// <param name="id">权限id</param>
        /// <param name="op">操作人</param>
        /// <returns></returns>
        bool DeletePrivilege(long id, long op);

        /// <summary>
        /// 激活权限
        /// </summary>
        /// <param name="id">权限id</param>
        /// <param name="op">操作人</param>
        /// <returns></returns>
        bool ActionPrivilege(long id, long op);
    }
}