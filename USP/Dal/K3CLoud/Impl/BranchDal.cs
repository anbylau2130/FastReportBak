using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.K3CLoud.Impl
{
    public class BranchDal : IBranchDal
    {
        K3CloudEntities db = new K3CloudEntities();
        public List<CMK_LS_Branch> GetBranchs()
        {
            return db.CMK_LS_Branch.ToList();
        }

        public List<CMK_LS_Branch_L> GetBranchsLanguage()
        {
            return db.CMK_LS_Branch_L.ToList();
        }

        public List<BranchSelect> GetBranchSelect()
        {
            var branchs = db.CMK_LS_Branch.ToList();
            var branchl = db.CMK_LS_Branch_L.ToList();
            var result = from B in branchs
                         join BL in branchl
                         on B.FID equals BL.FID
                         select new BranchSelect() { FNumber = B.FNUMBER, Name = BL.FNAME };
            return result.ToList();  
        }
    }
}