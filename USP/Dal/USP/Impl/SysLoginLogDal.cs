using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysLoginLogDal : ISysLoginLogDal
    {
        USPEntities db = new USPEntities();
        public List<SysLoginLog> showPage(long @operator, string startTime, string endTime)
        {
            try
            {
                var entitys = db.SysLoginLog.Where(x => x.Operator == @operator);

                if (!string.IsNullOrEmpty(startTime))
                {
                    var start = Convert.ToDateTime(startTime);

                    entitys = entitys.Where(x => x.Time >= start);
                }

                if (!string.IsNullOrEmpty(endTime))
                {
                  var   end = Convert.ToDateTime(endTime).AddDays(1);
                    //var end = Convert.ToDateTime(endTime);
                    //if (startTime == endTime)
                    //{
                    //    
                    //}
                    entitys = entitys.Where(x => x.Time <= end);
                }

                return entitys.OrderByDescending(x => x.Time).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<SysLoginLog>();
            }
        }

        public SysLoginLog GetModelById(long @operator)
        {
            return db.SysLoginLog.FirstOrDefault();
        }

        public int Add(SysLoginLog model)
        {
            int result;
            try
            {
                db.SysLoginLog.Add(model);
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                result = 0;
                LogUtil.Exception("ExceptionLogger", ex);
            }
            return result;
        }
    }
}