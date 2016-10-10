//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using USP.Context;
//using USP.Models.Entity;
//using USP.Models.POCO;
//using USP.Utility;

//namespace USP.Dal.Impl
//{
//    public class SysOperaterSupplierDal:ISysOperaterSupplierDal
//    {
//        USPEntities db = new USPEntities();
//        public ProcResult AddOperaterSupplier(SysOperatorSupplier model)
//        {
//            try
//            {
//                var result = db.UP_AddOperaterSupplier(model.Operator, model.Supplier, model.Creator,long.Parse(model.Reserve)).FirstOrDefault();

//                return new ProcResult()
//                {
//                    IsSuccess = result.Split('|')[0] == "true",
//                    ProcMsg = result.Split('|')[1]
//                };
//            }
//            catch (Exception ex)
//            {
//                LogUtil.Exception("ExceptionLogger", ex);
//                return  new ProcResult(); 
//            }

//        }

//        //public SysOperatorSupplier GetSupplierByOperator(long @operator)
//        //{
//        //   return db.SysOperatorSupplier.Where(x => x.Operator == @operator).FirstOrDefault();
//        //}
//    }
//}