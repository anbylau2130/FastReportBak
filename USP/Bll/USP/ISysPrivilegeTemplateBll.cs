using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysPrivilegeTemplateBll
    {
        List<SysPrivilegeTemplate> GetSysPrivilegeTemplateByCorpType(long corp);

        List<SysPrivilege> GetPrivilegeByParent(long id, List<SysPrivilege> allPrivileges);

        List<TreeNode> GetPrivilegeTreeList(long corpType, long id, List<SysPrivilege> allPrivileges, string privileges = "");
        List<SysPrivilege> GetPrivileges();
        AjaxResult SavePrivileges(long corpType, string privileges, long creator);
        AjaxResult AuditorPrivileges(long corpType, string privileges, long creator);

        DataGrid<UP_ShowPrivilegeTemplate_Result> GetPrivilegeTemplateGrid(int? pageIndex, int? pageSize, string corpType, string strOrder, string strOrderType);
    }
}
