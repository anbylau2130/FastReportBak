using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class OrganizationBll : IOrganizationBll
    {
         IOrganizationsDal dal;

        public OrganizationBll(IOrganizationsDal dal)
        {
            this.dal = dal;
        }

        public List<T_ORG_ORGANIZATIONS> GetAll()
        {
            return dal.GetAll();
        }

        public List<T_ORG_ORGANIZATIONS_L> GetAllLanguage()
        {
            return dal.GetAllLanguage();
        }
    }
}