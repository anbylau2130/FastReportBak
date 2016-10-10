using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class SysLoginLogBll : ISysLoginLogBll
    {
        USP.Dal.ISysLoginLogDal sysLoginLogDal;
        public SysLoginLogBll(USP.Dal.ISysLoginLogDal sysLoginLogDal)
        {
            this.sysLoginLogDal = sysLoginLogDal;
        }

        public List<SysLoginLog> getSysLoginLogGrid(long @operator, string startTime, string endTime)
        {
            return sysLoginLogDal.showPage(@operator, startTime, endTime);
        }

        public bool Add(SysLoginLog model)
        {
            return sysLoginLogDal.Add(model) == 0 ? false : true;
        }
    }
}