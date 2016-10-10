using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.Impl
{
    public class K3CloudProcDal : IK3CloudProcDal
    {
        private K3CloudEntities db = new K3CloudEntities();
        public List<UP_ShowOrder_Result> GetOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            return db.UP_ShowOrder(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
        }

        public List<UP_ShowPaymentOrder_Result> GetPaymentOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            return db.UP_ShowPaymentOrder(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
        }

        public List<UP_ShowStockIn_Result> GetStockInList(Nullable<int> pageIndex, Nullable<int> pageSize,
            string whereStr, string strOrder, string strOrderType)
        {
            return db.UP_ShowStockIn(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
        }

        public List<UP_ShowSuppilerAccount_Result> GetSupplierAccountList(string supplier, Nullable<System.DateTime> bDATE, Nullable<System.DateTime> eDATE, Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType)
        {
            return db.UP_ShowSuppilerAccount(supplier, bDATE, eDATE, pageIndex, pageSize, whereStr, strOrder,
                strOrderType).ToList();
        }
    }
}