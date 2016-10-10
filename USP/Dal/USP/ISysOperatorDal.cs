using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysOperatorDal
    {
        List<SysOperator> Login(string loginName, string password, string session, string ip);
        List<SysMenu> GetMenus(long @operator);
        List<SysPrivilege> GetPrivileges(long @operator);
        string CheckSso(long @operator, string session);

        SysCorp GetCorp(long corp);
        List<SysOperator> GetAllOperator();
        #region libei
        SysOperator GetModel(long @operator);
        int Add(SysOperator model);
        int Edit(SysOperator model);
        #endregion
    }
}
