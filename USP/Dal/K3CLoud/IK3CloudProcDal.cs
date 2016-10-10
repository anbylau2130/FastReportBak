using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface IK3CloudProcDal
    {
        List<UP_ShowOrder_Result> GetOrderList(Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType);
        List<UP_ShowStockIn_Result> GetStockInList(Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType);
        List<UP_ShowPaymentOrder_Result> GetPaymentOrderList(Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType);

        List<UP_ShowSuppilerAccount_Result> GetSupplierAccountList(string supplier,
            Nullable<System.DateTime> bDATE, Nullable<System.DateTime> eDATE, Nullable<int> pageIndex,
            Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType);
    }
}