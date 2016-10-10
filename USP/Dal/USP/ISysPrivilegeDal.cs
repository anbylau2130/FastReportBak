using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing.Constraints;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysPrivilegeDal
    {
        List<SysPrivilege> GetPrivilegeByOperator(long @operator);
        void AddPrivilege(string menu, string parent, string name, string @class, string area, string controller, string action, string parameter, string url);

        List<SysPrivilege> GetPrivilegeByRole(long @role);

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
        /// 根据公司获取角色列表
        /// </summary>
        List<SysPrivilegeExt> GetSysPrivilegePage(string strGetFields, string strWhere, string strOrder, int page, int pageSize);

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
