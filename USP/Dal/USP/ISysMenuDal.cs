using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysMenuDal
    {
        List<SysMenu> getMenuByOperator(long @operator);
        void addMenu(string name, string icon);
        void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url);

        List<SysMenu> getMenuByRole(long @role);

        List<SysMenu> GetAllMenus();

        #region libei
        List<Models.Entity.SysMenu> getMenuGrid(string name);
        SysMenu GetModel(long @id);
        bool IsExistMenuName(long id, long parent, string name);
        string Add(Models.Entity.SysMenu model);
        string Edit(Models.Entity.SysMenu model);
        int Audit(Models.Entity.SysMenu model);
        string Cancel(long id, long operater);
        //List<TreeNode> QueryTreeList(string id);
        #endregion
    }
}
