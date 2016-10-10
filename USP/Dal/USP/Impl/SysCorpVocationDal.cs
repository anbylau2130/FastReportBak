using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysCorpVocationDal : ISysCorpVocationDal
    {
        USPEntities db = new USPEntities();
        public List<SysCorpVocation> GetSysCorpVocations()
        {
            return db.SysCorpVocation.ToList();
        }
    }
}
