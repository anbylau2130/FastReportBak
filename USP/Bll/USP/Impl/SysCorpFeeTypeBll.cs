using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Bll.Impl
{
    public  class SysCorpFeeTypeBll: ISysCorpFeeTypeBll
    {
         ISysCorpFeeTypeDal sysCorpFeeTypeBllDal;


        public SysCorpFeeTypeBll(ISysCorpFeeTypeDal sysCorpFeeTypeBllDal)
        {
            this.sysCorpFeeTypeBllDal = sysCorpFeeTypeBllDal;
        }

        public List<SysCorpFeeType> GetCorpFeeTypeDropDownList()
        {
           return sysCorpFeeTypeBllDal.GetSysCorpFeeTypes();
        }


    }
}
