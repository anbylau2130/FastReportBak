using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysPrivilegeTemplateDal
    {
        List<UP_ShowPrivilegeTemplate_Result> GetPrivilegeTemplateGrid(int? pageIndex, int? pageSize, string corpType, string strOrder,
            string strOrderType);
        List<SysPrivilegeTemplate> GetPrivilegeTemplatesByCorpType(long corpType);

        List<SysPrivilege> GetPrivilegeByParent(long id);
        List<SysPrivilege> GetPrivileges();
        ProcResult SavePrivilegeTemplates(long corpType, string privileges,long creator);
        int AuditorPrivilegeTemplates(long corpType, string privileges, long creator);
    }
}
