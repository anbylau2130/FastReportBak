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
    public class SysCorpStatusDal : ISysCorpStatusDal
    {
        USPEntities db = new USPEntities();

        public List<SysCorpStatus> GetSysCorpStatus()
        {
            return db.SysCorpStatus.ToList();
        }
    }
}
