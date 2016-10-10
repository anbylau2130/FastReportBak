using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Service
{
    public interface ISysOperatorService
    {
        List<SysOperator> Login(string loginName, string password, string session, string ip);
        List<SysMenu> GetMenu(long @operator);
        List<SysPrivilege> GetPrivilege(long @operator);
        string CheckSso(long @operator, string session);

        SysCorp GetCorp(long corp);

        List<SysRole> GetRoles(Int64 corpid);
        SysOperator GetOperatorbyId(Int64 id);
        List<SysOperatorStatus> GetOperatorStatus();
    }
}
