using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISupplierGroupBll
    {
        List<T_BD_SUPPLIERGROUP> GetAll();
        List<T_BD_SUPPLIERGROUP_L> GetAllLanguage();
        List<TreeNode> GetSupplierGroupsByPId(long id);
    }
}