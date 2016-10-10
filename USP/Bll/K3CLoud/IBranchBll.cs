using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.K3CLoud
{
    public interface IBranchBll
    {
        List<CMK_LS_Branch> GetBranchs();
        List<CMK_LS_Branch_L> GetBranchLanguage();
        List<BranchSelect> GetBranchSelect();
    }
}
