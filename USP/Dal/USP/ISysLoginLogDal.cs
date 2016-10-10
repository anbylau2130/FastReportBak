using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysLoginLogDal
    {
        List<SysLoginLog> showPage(long @operator, string startTime, string endTime);
         int Add(SysLoginLog model);
    }
}