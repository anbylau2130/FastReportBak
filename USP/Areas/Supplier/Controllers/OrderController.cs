using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Controllers;
using USP.Models.POCO;

namespace USP.Areas.Supplier.Controllers
{
    [Menu(Name = "供应商对账平台", Icon = "icon-truck")]
    public class OrderController : SysPrivilegeController
    {
         IK3CloudProcBll k3CloudProcBll;
         ISupplierBll supplierBll;
        public OrderController(IK3CloudProcBll k3CloudProcBll, ISupplierBll supplierBll)
        {
            this.k3CloudProcBll = k3CloudProcBll;
            this.supplierBll = supplierBll;
        }
        [MenuItem(Parent = "供应商对账平台", Name = "采购单查询", Icon = "icon-paste")]
        // GET: Supplier/Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }
      

        [MenuItem(Parent = "供应商对账平台", Name = "入库单查询", Icon = "icon-paste")]
        public ActionResult StockInList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StockInList(string actionName)
        {
            return OtherAction(actionName);
        }


        [MenuItem(Parent = "供应商对账平台", Name = "付款单查询", Icon = "icon-paste")]
        // GET: Supplier/Order
        public ActionResult PaymentOrderList()
        {
            return View();
        }
        [MenuItem(Parent = "供应商对账平台", Name = "供应商对账", Icon = "icon-paste")]
        // GET: Supplier/Order
        public ActionResult SupplierAmount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SupplierAmount(string actionName)
        {
            return OtherAction(actionName);
        }
        [HttpPost]
        public ActionResult PaymentOrderList(string actionName)
        {
            return OtherAction(actionName);
        }
        #region GetDataGrid
        private ActionResult StockInGetDataGrid()
        {
            string wherestr = string.Empty;

            int page;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            int rows;
            if (!int.TryParse(Request["rows"], out rows))
            {
                rows = 10;
            }
            string type = Request["type"];
            string name = Request["name"];
            string beginTime = Request["beginTime"];
            string endTime = Request["endTime"];
            if (!string.IsNullOrEmpty(type))
            {
                wherestr += "  and [" + type + "] like '%" + name + "%' ";
            }
            if (beginTime.IsDateTime())
            {
                wherestr += "  and [入库日期] >='" + beginTime + "' ";
            }
            if (endTime.IsDateTime())
            {
                wherestr += "  and [入库日期] <= '" + endTime + "' ";
            }
            var currentUser = HttpContext.Session[Constants.USER_KEY] as User;
            if (currentUser != null && currentUser.SysOperator.ID != 0)
                wherestr += " and FSUPPLIERID='" + currentUser.Supplier.FSUPPLIERID + "'";
            var result = k3CloudProcBll.GetStockInList(page, rows, wherestr, "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private ActionResult OrderGetDataGrid()
        {
            string wherestr = string.Empty;
            int page;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            int rows;
            if (!int.TryParse(Request["rows"], out rows))
            {
                rows = 10;
            }
            string beginTime = Request["beginTime"];
            string endTime = Request["endTime"];
            string type = Request["type"];
            string name = Request["name"];
            if (!string.IsNullOrEmpty(type))
            {
                wherestr += "  and [" + type + "] like '%" + name + "%' ";
            }
            if (beginTime.IsDateTime())
            {
                wherestr += "  and [交货日期] >='" + beginTime + "' ";
            }
            if ( endTime.IsDateTime())
            {
                wherestr += "  and [交货日期] <= '" + endTime + "' ";
            }
            var currentUser = HttpContext.Session[Constants.USER_KEY] as User;
            if (currentUser != null && currentUser.SysOperator.ID != 0)
                wherestr += " and FSUPPLIERID='" + currentUser.Supplier.FSUPPLIERID + "'";
            var result = k3CloudProcBll.GetOrderList(page, rows, wherestr, "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private ActionResult PaymentOrderListGetDataGrid()
        {
            string wherestr = string.Empty;

            int page;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            int rows;
            if (!int.TryParse(Request["rows"], out rows))
            {
                rows = 10;
            }
            string beginTime = Request["beginTime"];
            string endTime = Request["endTime"];
            string type = Request["type"];
            string name = Request["name"];
            if (!string.IsNullOrEmpty(type))
            {
                wherestr += "  and [" + type + "] like '%" + name + "%' ";
            }
            if (beginTime.IsDateTime())
            {
                wherestr += "  and [业务日期] >='" + beginTime + "' ";
            }
            if (endTime.IsDateTime())
            {
                wherestr += "  and [业务日期] <= '" + endTime + "' ";
            }
            var currentUser = HttpContext.Session[Constants.USER_KEY] as User;
            if (currentUser != null && currentUser.SysOperator.ID != 0)
                wherestr += " and FSUPPLIER='" + currentUser.Supplier.FSUPPLIERID + "'";
            var result = k3CloudProcBll.GetPaymentOrderList(page, rows, wherestr, "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private ActionResult SupplierAmountGetDataGrid()
        {
            string wherestr = string.Empty;

            int page;
            if (!int.TryParse(Request["page"], out page))
            {
                page = 1;
            }
            int rows;
            if (!int.TryParse(Request["rows"], out rows))
            {
                rows = 10;
            }
            string date = Request["AmountDate"];
            if (date == string.Empty)
                date = DateTime.Now.Year + "-" + DateTime.Now.Month;
            DateTime beginDateTime = DateTime.Parse(date + "-01");
            int days = DateTime.DaysInMonth(beginDateTime.Year, beginDateTime.Month);
            DateTime endDateTime = DateTime.Parse(date + "-"+ days);
            var supplierid = string.Empty;

            var currentUser = HttpContext.Session[Constants.USER_KEY] as User;
            if (currentUser.Supplier != null)
            {
                
                   supplierid = currentUser.Supplier.FSUPPLIERID.ToString();
            }
            var result = k3CloudProcBll.GetSupplierAccountList(supplierid, beginDateTime, endDateTime, page, rows, wherestr, "", "");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
   

       

        private ActionResult OtherAction(string ac)
        {
            switch (ac)
            {
                case "orderdatagrid":
                    return OrderGetDataGrid();//列表页数据
                case "stockinlistdatagrid":
                    return StockInGetDataGrid();//列表页数据
                case "paymentorderlistdatagrid":
                    return PaymentOrderListGetDataGrid();//列表页数据
                case "supplieramountdatagrid":
                    return SupplierAmountGetDataGrid();//列表页数据
                default:
                    return Content("");
            }
        }
        #endregion
    }
}