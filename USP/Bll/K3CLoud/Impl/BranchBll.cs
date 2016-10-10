using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal.K3CLoud;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.K3CLoud.Impl
{
    public class BranchBll : IBranchBll
    {
        IBranchDal branchDal;

        public BranchBll(IBranchDal branch)
        {
            branchDal = branch;
        }

        public List<CMK_LS_Branch_L> GetBranchLanguage()
        {
            return branchDal.GetBranchsLanguage();
        }

        public List<CMK_LS_Branch> GetBranchs()
        {
            return branchDal.GetBranchs();
        }
        public List<BranchSelect> GetBranchSelect()
        {
            return branchDal.GetBranchSelect();
        }
    }
}