using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysMenuTemplateDal
    {
        List<SysMenuTemplate> getSysMenuTemplateList();
        List<UP_GetMenuTemplate_Result> getSysMenuTempGrid(long id);

        SysMenuTemplate GetModel(long @id);
        List<SysMenuTemplate> GetCorpList(long corp);
        int Add(long corpType, long creator, string menu);
        int Edit(long corpType, long creator, string menu);
        int Audit(long corpType, long creator, string menu);

    }
}