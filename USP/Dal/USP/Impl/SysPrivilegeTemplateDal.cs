using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysPrivilegeTemplateDal : ISysPrivilegeTemplateDal
    {
        USPEntities db = new USPEntities();
        public List<SysPrivilegeTemplate> GetPrivilegeTemplatesByCorpType(long corpType)
        {
            return db.SysPrivilegeTemplate.Where(x => x.CorpType == corpType).ToList();
        }

        public List<SysPrivilege> GetPrivilegeByParent(long id)
        {
            return db.SysPrivilege.Where(u => u.ID == id).ToList();
        }

        public List<SysPrivilege> GetPrivileges()
        {
            return db.SysPrivilege.ToList();
        }

        public ProcResult SavePrivilegeTemplates(long corpType, string privileges, long creator)
        {
            try
            {
                var result = db.UP_AddPrivilegeTemplate(corpType, privileges, creator).FirstOrDefault();
                return new ProcResult()
                {
                    IsSuccess = result.Split('|')[0] == "true",
                    ProcMsg = result.Split('|')[1]
                };
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new ProcResult();
            }
        }


        //public ProcResult AuditorPrivilegeTemplates(long corpType, string privileges, long creator)
        //{
        //    try
        //    {
        //        var result = db.UP_AuditorPrivilegeTemplate(corpType, privileges, creator);

        //        return new ProcResult()
        //        {
        //            IsSuccess = result.Split('|')[0] == "true",
        //            ProcMsg = result.Split('|')[1]
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtil.Exception("ExceptionLogger", ex);
        //        return new ProcResult();
        //    }
        //}


        public int AuditorPrivilegeTemplates(long corpType, string privileges, long creator)
        {
            return db.UP_AuditorPrivilegeTemplate(corpType, privileges, creator);
        }


        public List<UP_ShowPrivilegeTemplate_Result> GetPrivilegeTemplateGrid(int? pageIndex, int? pageSize, string corpType, string strOrder, string strOrderType)
        {
            try
            {
                return (from i in
                            db.UP_ShowPrivilegeTemplate(pageIndex, pageSize, corpType, strOrder, strOrderType).ToList()
                        select i).ToList();

            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<UP_ShowPrivilegeTemplate_Result>();
            }
        }
    }
}
