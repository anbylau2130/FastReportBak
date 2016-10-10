using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISupplierGroupDal
    {
            List<T_BD_SUPPLIERGROUP> GetAll();
            List<TreeNode> GetSupplierGroupsByPId(long id);
            List<T_BD_SUPPLIERGROUP_L> GetAllLanguage();
    }
}