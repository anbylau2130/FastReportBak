using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.POCO;
using USP.Models.Entity;
using USP.Service;
namespace USP.Bll.Impl
{
    public class SysAreaBll : ISysAreaBll
    {
        ISysAreaService sysAreaService;
        public SysAreaBll(ISysAreaService sysAreaService)
        {
            this.sysAreaService = sysAreaService;
        }

        public List<TreeNode> getAreaTree()
        {
            TreeNode result = new TreeNode();
            result.children = new List<TreeNode>();
            result.state = "closed";
            result.text = "系统区域";
            List<SysArea> areas = sysAreaService.getAll();
            foreach (SysArea sysArea in areas.Where(a => a.Parent == a.Code))
            {
                result.children.Add(wrapTree(sysArea, areas));
            }
            return new List<TreeNode>{result};
        }

        private TreeNode wrapTree(SysArea parent,List<SysArea> areas)
        {
            TreeNode result = new TreeNode();
            result.id = Int64.Parse(parent.Code);
            result.children = new List<TreeNode>();
            result.state = "closed";
            result.text = parent.Name;
            foreach (SysArea sysArea in areas.Where(a => a.Parent == parent.Code && a.Parent != a.Code))
            {
                result.children.Add(wrapTree(sysArea,areas));
            }
            return result;
        }
        public DataGrid<UP_ShowArea_Result> getAreaGrid(int page, int rows, string order, string orderType)
        {
            DataGrid<UP_ShowArea_Result> result = new DataGrid<UP_ShowArea_Result>();
            result.rows = sysAreaService.showPage(page, rows, order, orderType);
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }



        public List<SelectOption> GetProvinces(string selectedId)
        {
            return sysAreaService.GetProvinces(selectedId);
        }


        public List<SelectOption> GetAreasByParent(string parentId)
        {
            return sysAreaService.GetAreasByParent(parentId);
        }
        public List<SelectOption> GetAreasByParent(string parentId, string selectedId)
        {
            return sysAreaService.GetAreasByParent(parentId, selectedId);
        }

    }
}
