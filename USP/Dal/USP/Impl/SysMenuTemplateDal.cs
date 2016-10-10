using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysMenuTemplateDal : ISysMenuTemplateDal
    {

        USPEntities db = new USPEntities();

        public int Add(long corpType, long creator, string menu)
        {
            return db.UP_SaveMenuTemplate(corpType,creator,menu);
        }

        public int Edit(long corpType, long creator, string menu)
        {
            return db.UP_SaveMenuTemplate(corpType, creator, menu);
        }

        public int Audit(long corpType, long creator, string menu)
        {
            return db.UP_AuditMenuTemplate(corpType,creator,menu);
        }

        public SysMenuTemplate GetModel(long id)
        {
            return db.SysMenuTemplate.FirstOrDefault(x => x.ID == id);
        }

        public List<SysMenuTemplate> GetCorpList(long corp)
        {
            return db.SysMenuTemplate.Where(x=>x.CorpType==corp).ToList();
        }

        public List<SysMenuTemplate> getSysMenuTemplateList()
        {
            return db.SysMenuTemplate.ToList();
        }

        public List<UP_GetMenuTemplate_Result> getSysMenuTempGrid(long id)
        {
            return db.UP_GetMenuTemplate(id).ToList();
        }
    }
}