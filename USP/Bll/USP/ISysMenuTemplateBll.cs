using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysMenuTemplateBll
    {
        List<SysMenuTemplate> getSysMenuTemplateList();
        List<UP_GetMenuTemplate_Result> getSysMenuTempGrid(long id);
  
        SysMenuTemplate GetModel(long @id);
        List<SysMenuTemplate> GetCorpList(long corp);
        List<TreeNode> GetUserRoleMenuPrivilegeTree(long corpType);
        AjaxResult Add(long corpType,long creator,string menu);
        AjaxResult Edit(long corpType, long creator, string menu);
        AjaxResult Audit(long corpType, long creator, string menu);

    }
}