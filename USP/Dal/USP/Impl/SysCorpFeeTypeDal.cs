using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysCorpFeeTypeDal: ISysCorpFeeTypeDal
    {
        USPEntities db = new USPEntities();

        public List<SysCorpFeeType> GetSysCorpFeeTypes()
        {
            return db.SysCorpFeeType.ToList();
        }
    }
}
