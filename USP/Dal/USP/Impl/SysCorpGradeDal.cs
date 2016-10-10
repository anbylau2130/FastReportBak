using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysCorpGradeDal : ISysCorpGradeDal
    {
        USPEntities db = new USPEntities();

        public List<SysCorpGrade> GetSysCorpGrades()
        {
           return db.SysCorpGrade.ToList();
        }
    }
}
