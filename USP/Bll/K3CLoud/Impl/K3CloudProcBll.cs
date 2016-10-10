using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class K3CloudProcBll : IK3CloudProcBll
    {
        IK3CloudProcDal k3CloudProcDal;
        public K3CloudProcBll(IK3CloudProcDal k3CloudProcDal)
        {
            this.k3CloudProcDal = k3CloudProcDal;
        }

        public DataGrid<UP_ShowOrder_Result> GetOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            DataGrid<UP_ShowOrder_Result> result = new DataGrid<UP_ShowOrder_Result>();
            result.rows = k3CloudProcDal.GetOrderList(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
            if (result.rows.Count > 0)
            {
                result.total = result.rows[0].RowCnt.Value;
            }
            return result;
        }

        public DataGrid<UP_ShowPaymentOrder_Result> GetPaymentOrderList(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            DataGrid<UP_ShowPaymentOrder_Result> result = new DataGrid<UP_ShowPaymentOrder_Result>();
            result.rows = k3CloudProcDal.GetPaymentOrderList(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
            if (result.rows.Count > 0)
            {
                result.total = result.rows[0].RowCnt.Value;
            }
            return result;
        }

        public DataGrid<UP_ShowStockIn_Result> GetStockInList(int? pageIndex, int? pageSize, string whereStr, string strOrder,
            string strOrderType)
        {
            DataGrid<UP_ShowStockIn_Result> result = new DataGrid<UP_ShowStockIn_Result>();
            result.rows = k3CloudProcDal.GetStockInList(pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList();
            if (result.rows.Count > 0)
            {
                result.total = result.rows[0].RowCnt.Value;
            }
            return result;
        }


        public  DataGrid<UP_ShowSuppilerAccount_Result> GetSupplierAccountList(string supplier,DateTime? bDATE, DateTime? eDATE, int? pageIndex,int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            DataGrid<UP_ShowSuppilerAccount_Result> result = new DataGrid<UP_ShowSuppilerAccount_Result>();
            result.rows = k3CloudProcDal.GetSupplierAccountList(supplier,
                bDATE, eDATE, pageIndex,
                pageSize, whereStr, strOrder, strOrderType).ToList();
            if (result.rows.Count > 0)
            {
                result.total = result.rows[0].RowCnt.Value;
            }
            return result;
        }

    }
}