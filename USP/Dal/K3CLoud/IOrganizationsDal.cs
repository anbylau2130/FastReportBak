using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface IOrganizationsDal
    {
         List<Models.Entity.T_ORG_ORGANIZATIONS> GetAll();
         List<Models.Entity.T_ORG_ORGANIZATIONS_L> GetAllLanguage();
    }
}