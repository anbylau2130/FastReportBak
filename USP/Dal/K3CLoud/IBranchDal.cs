using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.K3CLoud
{
    public interface IBranchDal
    {
        List<CMK_LS_Branch> GetBranchs();

        List<CMK_LS_Branch_L> GetBranchsLanguage();
        List<BranchSelect> GetBranchSelect();
    }
}
