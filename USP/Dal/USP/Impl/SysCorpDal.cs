using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysCorpDal : ISysCorpDal
    {
        USPEntities db = new USPEntities();
        public List<UP_ShowCorp_Result> getCorps(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            try
            {
                return (from i in db.UP_ShowCorp(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList()
                        select i).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<UP_ShowCorp_Result>();
            }
        }

        public ProcResult AddCorporation(string corpName, long corpType, long @operator, long parentCorp, string loginName, string password)
        {
            try
            {
                var result = db.UP_AddCorp(corpName, corpType, @operator, parentCorp, loginName, password).FirstOrDefault();

                return new ProcResult()
                {
                    IsSuccess = result.Split('|')[0] == "true",
                    ProcMsg = result.Split('|')[1]
                };
            }
            catch (Exception exception)
            {
                LogUtil.Exception("ERROR", exception);
                return new ProcResult();
            }
        }

        public bool EditCorporation(SysCorp syscorp)
        {
            try
            {
                if (syscorp == null)
                {
                    return false;
                }
                if (db.Entry(syscorp).GetValidationResult().IsValid)
                {
                    db.Set<SysCorp>().Attach(syscorp);
                    db.Entry(syscorp).State = EntityState.Modified;
                    if (db.SaveChanges() == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public bool AuditorCorporation(SysCorp syscorp)
        {
            try
            {
                var model = db.SysCorp.FirstOrDefault(u => u.ID == syscorp.ID);
                if (db.Entry(syscorp).GetValidationResult().IsValid)
                {
                    db.Set<SysCorp>().Attach(syscorp);
                    db.Entry(syscorp).State = EntityState.Modified;
                    if (db.SaveChanges() == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public SysCorp GetCorporation(long ID)
        {
            return db.SysCorp.FirstOrDefault(u => u.ID == ID);
        }

        public int AuditorCorporation(long id, long auditor)
        {
            int result;
            try
            {
                var entity = GetCorporation(id);
                if (entity != null)
                {
                    entity.Auditor = auditor;
                    entity.AuditTime = DateTime.Now;
                    entity.Creator = auditor;

                    entity.Status = 0;
                }
                db.Entry<SysCorp>((SysCorp)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = -1;
            }
            return result;

        }

        public int CancelCorpotation(long id, long currentOperator)
        {
            int result;
            try
            {
                var entity = GetCorporation(id);
                if (entity != null)
                {
                    entity.Canceler = entity.Canceler;
                    entity.Creator = currentOperator;
                    entity.AuditTime = DateTime.Now;

                    entity.Status = 1;
                }
                db.Entry<SysCorp>((SysCorp)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public int EnableCorpotation(long id, long currentOperator)
        {
            int result;
            try
            {
                var entity = GetCorporation(id);
                if (entity != null)
                {
                    entity.Canceler = null;
                    entity.Creator = currentOperator;
                    entity.CancelTime = null;

                    entity.Status = 0;
                }
                db.Entry<SysCorp>((SysCorp)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        //public ProcResult AuditorCorporation(long id,long auditor)
        //{
        //    try
        //    {
        //        var result = db.UP_AuditorCorp(id, auditor).FirstOrDefault();

        //        return new ProcResult()
        //        {
        //            IsSuccess = result.Split('|')[0] == "true",
        //            ProcMsg = result.Split('|')[1]
        //        };
        //    }
        //    catch (Exception exception)
        //    {
        //        LogUtil.Exception("ExceptionLogger", exception);
        //        return new ProcResult();
        //    }

        //}

        //public ProcResult CancelCorpotation(long id, long currentOperator)
        //{

        //    var result=  db.UP_CancelCorp(id, currentOperator).FirstOrDefault();

        //    return new ProcResult()
        //    {
        //        IsSuccess = result.Split('|')[0] == "true",
        //        ProcMsg = result.Split('|')[1]
        //    };
        //}

        //public ProcResult EnableCorpotation(long id, long currentOperator)
        //{

        //    var result = db.UP_EnableCorp(id, currentOperator).FirstOrDefault();

        //    return new ProcResult()
        //    {
        //        IsSuccess = result.Split('|')[0] == "true",
        //        ProcMsg = result.Split('|')[1]
        //    };

        //}

    }
}