using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;


namespace USP.Bll
{
    public interface ISysLoginLogBll
    {
        List<SysLoginLog> getSysLoginLogGrid(long @operator, string startTime, string endTime);
        bool Add(SysLoginLog model);
    }
}