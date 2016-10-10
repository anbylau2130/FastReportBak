using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class SysPrivilegeTemplateBll : ISysPrivilegeTemplateBll
    {
        ISysPrivilegeTemplateDal sysPrivilegeTemplateDal;
        ISysMenuBll sysMenuBll;
        ISysMenuTemplateBll sysMenuTemplateBll;

        public SysPrivilegeTemplateBll(ISysPrivilegeTemplateDal sysPrivilegeTemplateDal, ISysMenuBll sysMenuBll, ISysMenuTemplateBll sysMenuTemplateBll)
        {
            this.sysPrivilegeTemplateDal = sysPrivilegeTemplateDal;
            this.sysMenuBll = sysMenuBll;
            this.sysMenuTemplateBll = sysMenuTemplateBll;
        }

        public List<SysPrivilegeTemplate> GetSysPrivilegeTemplateByCorpType(long corp)
        {
            return sysPrivilegeTemplateDal.GetPrivilegeTemplatesByCorpType(corp);
        }

        public List<SysPrivilege> GetPrivilegeByParent(long id, List<SysPrivilege> allPrivileges)
        {
            var list = (from i in allPrivileges
                        where i.Parent == id && i.ID != 0 && i.Canceler == null && i.CancelTime == null
                        select i).ToList();
            return list;
        }

        public List<TreeNode> GetPrivilegeTreeList(long corpType, long id, List<SysPrivilege> allPrivileges, string privileges = "")
        {
            List<TreeNode> treeNodes = new List<TreeNode>();
            var sysPrivileges = GetPrivilegeByParent(id, allPrivileges);
            //var menus = sysMenuBll.getMenuGrid("").Where(x => x.Canceler == null && x.CancelTime == null).ToList();
            // var menus = sysMenuTemplateBll.getSysMenuTempGrid(corpType); //sysMenuBll.getMenuGrid("").Where(x => x.Canceler == null && x.CancelTime == null).ToList();//测试着取roleMenu中Menu
            List<UP_GetMenuTemplate_Result> menus = new List<UP_GetMenuTemplate_Result>();
            menus = sysMenuTemplateBll.getSysMenuTempGrid(Convert.ToInt32(corpType));
            foreach (var item in sysPrivileges)
            {
                var treeNode = new TreeNode()
                {
                    id = item.ID,
                    text = item.Name,
                };
                var menu = menus.FirstOrDefault(x => x.Menu == item.Menu);
                if (menu != null)
                {
                    treeNode.text = menu.MenuName + ">>" + item.Name;
                }
                else
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(privileges) && privileges.Split(',').Contains(item.ID.ToString()))
                {
                    treeNode.@checked = true;
                }
                if (GetPrivilegeByParent(item.ID, allPrivileges).Count > 0)
                {
                    treeNode.children = GetPrivilegeTreeList(corpType, item.ID, allPrivileges, privileges);
                }
                treeNodes.Add(treeNode);
            }

            return treeNodes;
        }

        public List<SysPrivilege> GetPrivileges()
        {
            return sysPrivilegeTemplateDal.GetPrivileges();
        }

        public AjaxResult SavePrivileges(long corpType, string privileges, long creator)
        {
            var procResult = sysPrivilegeTemplateDal.SavePrivilegeTemplates(corpType, privileges, creator);
            return new AjaxResult()
            {
                dateTime = DateTime.Now,
                flag = procResult.IsSuccess,
                message = procResult.ProcMsg
            };
        }

        //public AjaxResult AuditorPrivileges(long corpType, string privileges, long creator)
        //{
        //    var result = sysPrivilegeTemplateDal.AuditorPrivilegeTemplates(corpType, privileges, creator);
        //    return new AjaxResult()
        //    {
        //        dateTime = DateTime.Now,
        //        flag = result.IsSuccess,
        //        message = result.ProcMsg
        //    };
        //}

        public AjaxResult AuditorPrivileges(long corpType, string privileges, long creator)
        {
            AjaxResult result = new AjaxResult();
            if (sysPrivilegeTemplateDal.AuditorPrivilegeTemplates(corpType, privileges, creator) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，审核成功！";
            }
            else
            {
                result.flag = false;
                result.message = "审核失败！";
            }
            return result;
        }

        public DataGrid<UP_ShowPrivilegeTemplate_Result> GetPrivilegeTemplateGrid(int? pageIndex, int? pageSize, string corpType, string strOrder, string strOrderType)
        {
            DataGrid<UP_ShowPrivilegeTemplate_Result> result = new DataGrid<UP_ShowPrivilegeTemplate_Result>();
            result.rows = sysPrivilegeTemplateDal.GetPrivilegeTemplateGrid(pageIndex, pageSize, corpType, strOrder, strOrderType);
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }
    }
}
