using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.POCO;
using USP.Models.Entity;
namespace USP.Bll
{
    public interface ISysAreaBll
    {
        List<TreeNode> getAreaTree();
        DataGrid<UP_ShowArea_Result> getAreaGrid(int page, int rows, string order, string orderType);



        List<SelectOption> GetProvinces(string selectId);

        List<SelectOption> GetAreasByParent(string parentId);
        List<SelectOption> GetAreasByParent(string parentId, string selectedId);
    }
}
