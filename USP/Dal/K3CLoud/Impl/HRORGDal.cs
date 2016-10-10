using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.K3CLoud.Impl
{
    public class HRORGDal : IHRORGDal
    {
        K3CloudEntities db = new K3CloudEntities();
        public List<HXN_HR_ORG> GetORGAll()
        {
            return db.HXN_HR_ORG.ToList();
        }

        public List<HXN_HR_ORG_Group> GetORGGroupAll()
        {
            return db.HXN_HR_ORG_Group.ToList();
        }

        public List<HXN_HR_ORG_Group_L> GetORGGroupLanguageAll()
        {
            return db.HXN_HR_ORG_Group_L.ToList();
        }

        public List<HXN_HR_ORG_L> GetORGLanguageALL()
        {
            return db.HXN_HR_ORG_L.ToList();
        }
    }
}