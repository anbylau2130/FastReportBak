//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using USP.Context;
//using USP.Models.Entity;
//using USP.Models.POCO;

//namespace USP.Dal.Impl
//{
//    public class SupplierDal : ISupplierDal
//    {
//        K3CloudEntities db = new K3CloudEntities();
//        USPEntities uspdb=new USPEntities();
//        public List<Models.Entity.T_BD_SUPPLIER> GetAll()
//        {
//            return db.T_BD_SUPPLIER.ToList();
//        }

//        public List<TreeNode> GetSuppliersByGroup(long id)
//        {
//            var result = from x in db.T_BD_SUPPLIER
//                join j in db.T_BD_SUPPLIER_L on x.FSUPPLIERID equals j.FSUPPLIERID
//                where x.FPRIMARYGROUP == id && x.FDOCUMENTSTATUS=="C"
//                select new TreeNode {id = x.FSUPPLIERID,  text =x.FNUMBER+"("+ j.FNAME+")" };
//            return result.ToList();
//        }

//        //public T_BD_SUPPLIER GetSupplierByOperatorId(long id)
//        //{

//        //    var result = from x in uspdb.SysOperatorSupplier.ToList()
//        //                 join y in db.T_BD_SUPPLIER on x.Supplier equals y.FSUPPLIERID
//        //                 where x.Operator==id
//        //                 select y;

//        //    return result.ToList().FirstOrDefault();

//        //}

//        public List<T_BD_SUPPLIER_L> GetAllLanguage()
//        {
//            return db.T_BD_SUPPLIER_L.ToList();
//        }
//    }
//}