using System.Collections.Generic;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysRoleDal
    {
        List<SysRole> GetSysRoleByOperator(long @operator);

        bool AddRole(long corp, bool type, string name, string remark, string menus, string privileges, long creator);

        bool EditRole(long id, string name, string remark, string menus, string privileges, long creator);

        SysRole GetRoleByID(long id);

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        bool CheckRoleName(string name, long corp);

        /// <summary>
        /// 根据公司获取角色列表
        /// </summary>
        List<SysRoleExt> GetSysRolePage(string strGetFields, string strWhere, string strOrder, int page, int pageSize);

        bool CancelOrActionRole(long roleId, long @operator);

        /// <summary>
        /// 根据公司查询角色
        /// </summary>
        /// <param name="corpid"></param>
        /// <returns></returns>
        List<SysRole> getRolesByCrop(long corpid);
    }
}
