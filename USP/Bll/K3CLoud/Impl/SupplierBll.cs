using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class SupplierBll : ISupplierBll
    {
        ISupplierDal supplierDal;
        ISupplierGroupDal supplierGroupDal;

        public SupplierBll(ISupplierDal dal,ISupplierGroupDal groupDal)
        {
            this.supplierDal = dal;
            this.supplierGroupDal = groupDal;
        }

        public List<T_BD_SUPPLIER> GetAll()
        {
            return supplierDal.GetAll();
        }

        public T_BD_SUPPLIER GetSupplierByOperatorId(long id)
        {
            return supplierDal.GetSupplierByOperatorId(id);
        }

        public List<TreeNode> GetSuppliersByGroup(long id)
        {
            return supplierDal.GetSuppliersByGroup(id);
        }

        public List<T_BD_SUPPLIER_L> GetAllLanguage()
        {
            return supplierDal.GetAllLanguage();
        }

        
    }
}