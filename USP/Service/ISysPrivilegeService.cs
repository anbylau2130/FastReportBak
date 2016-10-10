using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Service
{
    public interface ISysPrivilegeService
    {
        List<SysPrivilege> getPrivilegeByOperator(long @operator);
        void addPrivilege(string menu, string parent, string name, string @class, string area, string controller, string action, string parameter, string url);
        int AssignPrivilege();
    }
}
