using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Bll.Impl
{
    public class SysCorpVocationBll: ISysCorpVocationBll
    {
         ISysCorpVocationDal sysCorpVocationBll;

        public SysCorpVocationBll(ISysCorpVocationDal sysCorpVocationBll)
        {
            this.sysCorpVocationBll = sysCorpVocationBll;
        }

        public List<SysCorpVocation> GetSysCorpVocationDropDownList()
        {
            return sysCorpVocationBll.GetSysCorpVocations();
        }
    }
}
