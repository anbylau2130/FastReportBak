using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using USP.Context;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    public class SysMenuService : ISysMenuService
    {
        USPEntities db = new USPEntities();
        public List<SysMenu> getMenuByOperator(long @operator)
        {
            return db.UP_GetOperatorMenu(@operator).ToList();
        }
        public void addMenu(string name, string icon)
        {
            db.UP_AddMenu(name, icon);
        }
        public void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url)
        {
            db.UP_AddMenuItem(parent, name, icon, @class, area, controller, action, parameter, url);
        }


        public int AssignMenu()
        {
            StringBuilder sqlstr=new    StringBuilder();
            sqlstr.Append(" insert into SysRoleMenu(Role, Menu, Creator, Auditor) select 0, ID, 0, 0 from SysMenu ");
            sqlstr.Append(" WHERE ID NOT IN(SELECT Menu FROM dbo.SysRoleMenu WHERE role = 0) ");

            return  db.Database.ExecuteSqlCommand(sqlstr.ToString());
        }
    }
}
