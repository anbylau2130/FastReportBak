using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysCorpTypeDal : ISysCorpTypeDal
    {
        USPEntities db = new USPEntities();

        public List<SysCorpType> GetSysCorpTypes()
        {
            return db.SysCorpType.Where(x => x.Canceler == null).ToList();
        }

        public bool IsExisName(int id, string name)
        {
            return db.SysCorpType.Where(x => x.ID != id && x.Name == name).Count() > 0 ? true : false;
        }

        public List<SysCorpType> GetSysCorpTypes(string name)
        {
            var entity = db.SysCorpType.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                entity = entity.Where(x => x.Name.Contains(name)).ToList();
            }
            return entity;
        }

        public SysCorpType GetModelById(long corpType)
        {
            return db.SysCorpType.FirstOrDefault(x => x.ID == corpType);
        }

        public bool Add(SysCorpType model)
        {
            int result;
            try
            {
                db.SysCorpType.Add(model);
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                result = 0;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result > 0 ? true : false;
        }

        public bool Edit(SysCorpType model)
        {
            int result;
            try
            {
                var entity = GetModelById(model.ID);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Remark = model.Remark;
                    entity.Creator = model.Creator;
                    entity.CreateTime = model.CreateTime;

                }
                db.Entry<SysCorpType>((SysCorpType)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result > 0 ? true : false;
        }

        public int Cancel(int id, long @operator)
        {
            int result;
            try
            {
                var entity = GetModelById(id);
                if (entity != null)
                {
                    entity.Canceler = @operator;
                    entity.CancelTime = DateTime.Now;
                }
                db.Entry<SysCorpType>((SysCorpType)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public int Active(int id, long @operator)
        {
            int result;
            try
            {
                var entity = GetModelById(id);
                if (entity != null)
                {
                    entity.Canceler = null;
                    entity.CancelTime = null;
                    entity.Creator = @operator;
                    entity.CreateTime = DateTime.Now;
                }
                db.Entry<SysCorpType>((SysCorpType)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }
    }
}
