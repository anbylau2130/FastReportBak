using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Bll.Impl
{
    public class SysCorpGradeBll: ISysCorpGradeBll
    {
         ISysCorpGradeDal sysCorpGradeDal;

        public SysCorpGradeBll(ISysCorpGradeDal sysCorpGradeDal)
        {
            this.sysCorpGradeDal = sysCorpGradeDal;
        }

        public List<SysCorpGrade> GetSysCorpGradeDropDownList()
        {
            return sysCorpGradeDal.GetSysCorpGrades();

        }
    }
}
