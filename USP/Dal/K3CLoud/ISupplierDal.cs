using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISupplierDal
    {
        List<T_BD_SUPPLIER> GetAll();
         List<TreeNode> GetSuppliersByGroup(long id);

        T_BD_SUPPLIER GetSupplierByOperatorId(long id);
        List<T_BD_SUPPLIER_L> GetAllLanguage();

    }
}