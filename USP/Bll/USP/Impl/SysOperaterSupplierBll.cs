//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using USP.Dal;
//using USP.Models.Entity;
//using USP.Models.POCO;

//namespace USP.Bll.Impl
//{
//    public class SysOperaterSupplierBll : ISysOperaterSupplierBll
//    {
//        ISysOperaterSupplierDal sysOperaterSupplier;
//        public SysOperaterSupplierBll(ISysOperaterSupplierDal sysOperaterSupplier)
//        {
//            this.sysOperaterSupplier = sysOperaterSupplier;
//        }

//        public ProcResult AddOperaterSupplier(SysOperatorSupplier model)
//        {
//            return sysOperaterSupplier.AddOperaterSupplier(model);
//        }

//        public SysOperatorSupplier GetSupplierByOperator(long @operator)
//        {
//            return sysOperaterSupplier.GetSupplierByOperator(@operator);
//        }
//    }
//}