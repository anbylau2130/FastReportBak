using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.Impl
{
    public class OrganizationsDal : IOrganizationsDal
    {
        K3CloudEntities db = new K3CloudEntities();
        public List<Models.Entity.T_ORG_ORGANIZATIONS> GetAll()
        {
            return db.T_ORG_ORGANIZATIONS.ToList();
        }

        public List<Models.Entity.T_ORG_ORGANIZATIONS_L> GetAllLanguage()
        {
            return db.T_ORG_ORGANIZATIONS_L.ToList();
        }


    }
}