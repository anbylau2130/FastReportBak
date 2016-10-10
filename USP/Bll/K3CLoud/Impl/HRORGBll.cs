using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal.K3CLoud;
using USP.Models.Entity;

namespace USP.Bll.K3CLoud.Impl
{
    public class HRORGBll : IHRORGBll
    {

        IHRORGDal dal;

        public HRORGBll(IHRORGDal orgdal)
        {
            this.dal = orgdal;
        }

        public List<HXN_HR_ORG> GetORGAll()
        {
            return dal.GetORGAll();
        }

        public List<HXN_HR_ORG_Group> GetORGGroupAll()
        {
            return dal.GetORGGroupAll();
        }

        public List<HXN_HR_ORG_Group_L> GetORGGroupLanguageAll()
        {
            return dal.GetORGGroupLanguageAll();
        }

        public List<HXN_HR_ORG_L> GetORGLanguageALL()
        {
            return dal.GetORGLanguageALL();
        }
    }
}