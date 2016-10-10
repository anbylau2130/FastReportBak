using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface IK3CloudProcBll
    {
        DataGrid<UP_ShowOrder_Result> GetOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder,
            string strOrderType);

        DataGrid<UP_ShowStockIn_Result> GetStockInList(int? pageIndex, int? pageSize, string whereStr, string strOrder,
            string strOrderType);

        DataGrid<UP_ShowPaymentOrder_Result> GetPaymentOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder,
            string strOrderType);

        DataGrid<UP_ShowSuppilerAccount_Result> GetSupplierAccountList(string supplier,
            DateTime? bDATE, DateTime? eDATE, int? pageIndex,
            int? pageSize, string whereStr, string strOrder, string strOrderType);
    }
}