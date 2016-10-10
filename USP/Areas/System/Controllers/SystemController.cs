using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebSockets;
using USP.Bll;
using USP.Attributes;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;
using System.Transactions;
using USP.Bll.K3CLoud;

namespace USP.Areas.System.Controllers
{
    [Menu(Name = "系统设置", Icon = "panel-icon  icon-cogs")]
    public class SystemController : Controller
    {
        ISystemBll systemBll;
        //ISupplierBll supplierBll;
        ISysOperatorBll operatorBll;
        //ISupplierGroupBll supplierGroupBll;
        //ISysOperaterSupplierBll operaterSupplierBll;
        IEmployeeBll employeeBll;
        public SystemController(ISystemBll systemBll, IEmployeeBll employeeBll,/* ISupplierBll supplierBll, ISupplierGroupBll supplierGroupBll,
            ISysOperaterSupplierBll operaterSupplierBll,*/ ISysOperatorBll operatorBll)
        {
            this.employeeBll = employeeBll;
            this.systemBll = systemBll;
            //this.supplierBll = supplierBll;
            //this.supplierGroupBll = supplierGroupBll;
            //this.operaterSupplierBll = operaterSupplierBll;
            this.operatorBll = operatorBll;
        }


        // GET: System/System
        public ActionResult Index()
        {
            return View();
        }



        //[MenuItem(Parent = "系统数据", Name = "控制器数据", Icon = "glyphicon glyphicon-info-sign")]
        public ActionResult Controller()
        {
            return View();
        }


        public ActionResult Controllers()
        {
            return Json(systemBll.getControllerGrid(), JsonRequestBehavior.AllowGet);
        }

        //[MenuItem(Parent = "系统数据", Name = "菜单数据", Icon = "glyphicon glyphicon-list")]
        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult Menus()
        {
            return Json(systemBll.getMenuGrid(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Privileges()
        {
            return Json(systemBll.getPrivilegeGrid(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignMenu()
        {
            return Json(new
            {
                Count = systemBll.AssignMenu(),
                Msg = "菜单已分配给超级管理员"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignPrivilege()
        {
            return Json(new
            {
                Count = systemBll.AssignPrivilege(),
                Msg = "权限已分配给超级管理员"
            }
                , JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUsers()
        {
            var emps=employeeBll.GetEmployees();
            var emplanguages = employeeBll.GetEmployeeNames();
            List<OperaterAddEdit> operaterModelList = new List<OperaterAddEdit>();
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (var item in emps)
                {
                    if (operatorBll.GetOperatorbyLoginName(item.FNUMBER) == null)
                    {
                        var empL=(from i in emplanguages
                                      where i.FID == item.FID
                                                select i).FirstOrDefault();
                         var operaterModel = new OperaterAddEdit()
                         {
                             Corp = 1,
                             LoginName = item.FNUMBER,
                             RealName = empL.FNAME,
                             Password = Util.GetPassword(item.FNUMBER, "123456"),
                             IdCard =  item.FNUMBER,
                             Email = item.FNUMBER,
                             Mobile = item.FNUMBER,
                             Creator = 0,
                             Role = "2"
                         };
                        operaterModelList.Add(operaterModel);
                        operatorBll.SaveOperator(operaterModel);
                    }
                }
                ts.Complete();
            }
            return Json(operaterModelList, JsonRequestBehavior.AllowGet);
           
        }
        //public ActionResult Suppliers()
        //{
        //    var suppliers = supplierBll.GetAll();
        //    var supplierLanguages = supplierBll.GetAllLanguage();
        //    var groupLanguages = supplierGroupBll.GetAllLanguage();
        //    List<OperaterAddEdit> operaterModelList = new List<OperaterAddEdit>();
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        foreach (var item in suppliers)
        //        {
        //            if (operatorBll.GetOperatorbyLoginName("supplier" + item.FSUPPLIERID) == null)
        //            {
        //                var supplierlanguage = (from i in supplierLanguages
        //                                        where i.FSUPPLIERID == item.FSUPPLIERID
        //                                        select i).FirstOrDefault();
        //                var supplierGroupLanguge = (from i in groupLanguages
        //                                            where i.FID == item.FUSEORGID
        //                                            select i).FirstOrDefault();
        //                var realName = supplierGroupLanguge != null
        //                    ? supplierGroupLanguge.FNAME
        //                    : string.Empty + "(" + supplierlanguage != null ? supplierlanguage.FNAME : string.Empty + ")"
        //                    ;
        //                var operaterModel = new OperaterAddEdit()
        //                {
        //                    Corp = 1,
        //                    LoginName = "supplier" + item.FSUPPLIERID,
        //                    RealName = realName,
        //                    Password = Util.GetPassword("supplier" + item.FSUPPLIERID, "123456"),
        //                    IdCard = "supplier" + item.FSUPPLIERID,
        //                    Email = "supplier" + item.FSUPPLIERID,
        //                    Mobile = "supplier" + item.FSUPPLIERID,
        //                    Creator = 0,
        //                    Role = "2"
        //                };
        //                operatorBll.SaveOperator(operaterModel);
        //                var @operator = operatorBll.GetOperatorbyLoginName("supplier" + item.FSUPPLIERID);
        //                var relationModel = new SysOperatorSupplier()
        //                {
        //                    Operator = @operator.ID,
        //                    Creator = 0,
        //                    Auditor = 0,
        //                    AuditTime = DateTime.Now,
        //                    CreateTime = DateTime.Now,
        //                    Supplier = item.FSUPPLIERID,
        //                    Reserve = item.FUSEORGID.ToString()
        //                };
        //                operaterSupplierBll.AddOperaterSupplier(relationModel);
        //                operaterModel.ID = @operator.ID;
        //                operaterModelList.Add(operaterModel);
        //            }
        //        }
        //        ts.Complete();
        //    }
        //    return Json(operaterModelList, JsonRequestBehavior.AllowGet);
        //}
    }
}
