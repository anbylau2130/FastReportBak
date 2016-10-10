using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Service.Impl
{
    public class SysPrivilegeService : ISysPrivilegeService
    {
        USPEntities db = new USPEntities();
        public List<SysPrivilege> getPrivilegeByOperator(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }
        public void addPrivilege(string menu, string parent, string name, string @class, string area, string controller, string action, string parameter, string url)
        {
            db.UP_AddPrivilege(menu, parent, name, @class, area, controller, action, parameter, url);
        }

        public int AssignPrivilege()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(
                "  insert into SysRolePrivilege(Role, Privilege, Creator, Auditor) select 0, ID, 0, 0 from SysPrivilege ");
            sqlstr.Append(" WHERE id NOT IN(SELECT privilege FROM SysRolePrivilege WHERE Role = 0) ");
            return db.Database.ExecuteSqlCommand(sqlstr.ToString());
        }
    }
}
