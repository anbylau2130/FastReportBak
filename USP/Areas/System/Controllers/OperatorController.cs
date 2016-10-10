using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.WebPages;
using USP.Bll;
using USP.Attributes;
using USP.Models.POCO;
using USP.Common;
using USP.Controllers;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Areas.System.Controllers
{
    public class OperatorController : SysPrivilegeController
    {
        ISysOperatorBll operatorBll;
        //ISupplierBll supplierBll;
        //ISupplierGroupBll supplierGroupBll;
        //ISysOperaterSupplierBll sysoperatersupplier;
         //IOrganizationBll orgbll;
       
        public OperatorController(ISysOperatorBll operatorBll)
            //,ISupplierBll supplierBll,ISupplierGroupBll supplierGroupBll,
             //IOrganizationBll orgbll)
        {
            this.operatorBll = operatorBll;
            //this.supplierBll = supplierBll;
            //this.supplierGroupBll = supplierGroupBll;
           // this.sysoperatersupplier = sysoperatersupplier;
            //this.orgbll = orgbll;
        }

        [MenuItem(Parent = "系统设置", Name = "员工管理", Icon = "glyphicon glyphicon-user")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac)
            {
                case "datagrid":
                    return GetDataGrid();//列表页数据
                case "Combobox":
                    return GetStatusComboBox();//列表页状态combotree
                //case "orgtree":
                    
                //    return GetOrganizationTree();
                case "menutree":
                    return GetRoleTree();//加载添加管理员页面的角色tree
                case "checkname":
                    return CheckName();
                //case "suppliertree":
                //    var org = int.Parse(Request["orgid"]);
                //    var temp = GetSupplierTreeNode(org);
                //    return Json(temp);
                default:
                    return Content("");
            }
        }

       
        private ActionResult GetDataGrid()
        {
            string userName = null;
            string RealName = null;

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
            long status = Convert.ToInt64(Request["status"]);
            string type = Request["type"];
            if (type.ToLower() == "loginname")
            {
                userName = Request["name"];
            }
            else if (type.ToLower() == "realname")
            {
                RealName = Request["name"];
            }
            var operator1 = (User)HttpContext.Session[Common.Constants.USER_KEY];
            var corp = operator1.SysCorp.ID;
            var operatorID = operator1.SysOperator.ID;
           // var result = operatorBll.GetOperatorPageData( page, rows,  userName, RealName, operator1.SysCorp.ID, status, corp, operatorID.ToString());
            var result = operatorBll.GetOperatorGrid(page, rows, "", "", userName, RealName, -1, status, corp, operatorID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetStatusComboBox()
        {
            var status = operatorBll.GetOperatorStatus();
            List<SelectOption> list = new List<SelectOption>();
            var all = new SelectOption()
            {
                id = "-1",
                text = "全部",
                selected = true
            };
            list.Add(all);
            foreach (var v in status)
            {
                var temp = new SelectOption()
                {
                    id = v.ID.ToString(),
                    text = v.Name,
                    selected = false
                };
                list.Add(temp);
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        private ActionResult GetRoleTree()
        {
            var stropid = Request["opid"];
            long? opid = null;
            long temp;
            if (long.TryParse(stropid, out temp))
            {
                opid = temp;
            }
            var user = Session[Constants.USER_KEY] as User;
            var tree = operatorBll.GetUserRoleTree(user.SysCorp.ID, opid);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        private ActionResult CheckName()
        {
            var loginname= Request["name"];
            var result = new AjaxResult();
            if (operatorBll.GetOperatorbyLoginName(loginname) != null)
            {
                result.flag = false;
                result.message = "登录名已存在";
                return Json(result);
            }
            result.flag = true;
            result.message = "";
            return Json(result);
        }

        [Privilege(Menu = "员工管理", Name = "新增")]
        public ActionResult Create()
        {
            var operator1 = (User)HttpContext.Session[Common.Constants.USER_KEY];
            var corp = operator1.SysCorp.ID;
            return View();
        }

        [HttpPost]
        public ActionResult Create(OperaterAddEdit operators)
        {
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);
            if (ModelState.IsValid)
            {
                var user = Session[Constants.USER_KEY] as User;
                operators.Corp = user.SysCorp.ID;
                operators.Creator = user.SysOperator.ID;
                operators.Password = Util.GetPassword(operators.LoginName, operators.Password.Trim());
                operators.Email = operators.LoginName;
                operators.IdCard = operators.LoginName;
                operators.Mobile = operators.LoginName;
                operators.Role = operators.Role;
                //if (operatorBll.GetOperatorbyLoginName(operators.LoginName) != null)
                //{
                //    ModelState.AddModelError("LoginName", "当前用户已存在");
                //    return View(operators);
                //}
                if (!operatorBll.SaveOperator(operators))
                {
                    ModelState.AddModelError("errorresult", "添加失败");
                    TempData["resultMsgType"] = "error";
                    TempData["resultMsg"] = "添加失败";
                    return View(operators);
                }
                TempData["resultMsgType"] = "success";
                TempData["resultMsg"] = "添加成功";
                return RedirectToAction("Index");
            }
            return View(operators);
        }

        //[Privilege(Menu = "员工管理", Name = "关联供应商")]
        //public ActionResult RelationSupplier()
        //{
        //    var opid = Convert.ToInt64(Request["id"]);
        //    var operators = operatorBll.GetOperatorbyId(opid);
        //    if (operators != null)
        //    {
        //        var model = new OperatorSupplier();
        //        model.ID = operators.ID;
        //        model.LoginName = operators.LoginName;
        //        model.RealName = operators.RealName;
        //       // var relation = sysoperatersupplier.GetSupplierByOperator(operators.ID);
        //        //if (relation!=null)
        //        //{ 
        //        //    model.Supplier = relation.Supplier;
        //        //    long organization;
        //        //    if (long.TryParse(relation.Reserve, out organization))
        //        //    {
        //        //        model.Orgnization = organization;
        //        //    }
        //        //}
        //        //return View(model);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //[HttpPost]
        //public ActionResult RelationSupplier(OperatorSupplier model)
        //{
        //    var ac = Request["actionName"] ?? "";
        //    if (ac != "")
        //        return OtherAction(ac);
        //    var user = Session[Constants.USER_KEY] as User;
        //    //var result = sysoperatersupplier.AddOperaterSupplier(new SysOperatorSupplier() { Operator = model.ID, Supplier = model.Supplier, Creator = user.SysOperator.ID,Reserve = model.Orgnization.ToString()});
         
        //    //if (!result.IsSuccess)
        //    //{
        //    //    TempData["returnMsgType"] = "error";
        //    //    TempData["returnMsg"] = result.ProcMsg;
        //    //    return View(model);
        //    //}
        //    //TempData["resultMsgType"] = "success";
        //    //TempData["resultMsg"] = result.ProcMsg;
        //    return RedirectToAction("Index");

        //}

        [Privilege(Menu = "员工管理", Name = "修改")]
        public ActionResult Edit()
        {
            var opid = Convert.ToInt64(Request["id"]);
            var operators = operatorBll.GetOperatorbyId(opid);
            if (operators != null)
            {
                var model = new OperaterAddEdit();
                model.ID = operators.ID;
                model.LoginName = operators.LoginName;
                model.RealName = operators.RealName;
                model.Email = operators.Email;
                model.Mobile = operators.Mobile;
                model.IdCard = operators.IdCard;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(OperaterAddEdit model)
        {
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);
            //if (ModelState.IsValid)
            //{
            var operators = operatorBll.GetOperatorbyId(model.ID);
            model.Creator = ((User)Session[Constants.USER_KEY]).SysOperator.ID;
            if (operators == null)
            {
                RedirectToAction("Index", "Operator");
            }
            var result = operatorBll.EditOperator(model);
            if (!result.IsSuccess)
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = result.ProcMsg;
                return View(model);
            }
            TempData["resultMsgType"] = "success";
            TempData["resultMsg"] = result.ProcMsg;
            return RedirectToAction("Index");
            //}
            //return View();
        }

        [HttpPost]
        public ActionResult InitRole()
        {
            var user = Session[Constants.USER_KEY] as User;
            var result = new List<SysRole>();
            result = operatorBll.GetRoleList(Convert.ToInt64(user.SysCorp.ID));
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //审核
        [HttpPost]
        [Privilege(Menu = "员工管理", Name = "审核")]
        public ActionResult Auditor(long opid)
        {
            var result = new AjaxResult();
            int status = 0;
            int type = 3;
            var operatorModel = operatorBll.GetOperatorbyId(opid);
            if (operatorModel == null)
            {
                result.flag = false;
                result.message = "未找到操作员信息";
                return Json(result);
            }
            if (operatorModel.Auditor != null)
            {
                result.flag = false;
                result.message = "操作员已通过审核，无需重复操作！";
                return Json(result);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (operatorBll.AlterStatus(operatorModel, status, user.SysOperator.ID, type) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "审核通过失败";
            return Json(result);
        }

        //注销
        [HttpPost]
        [Privilege(Menu = "员工管理", Name = "注销")]
        public ActionResult Cancel(long opid)
        {
            var result = new AjaxResult();
            int status = 2;
            int type = 2;
            var operatorModel = operatorBll.GetOperatorbyId(opid);
            if (operatorModel == null)
            {
                result.flag = false;
                result.message = "未找到操作员信息";
                return Json(result);
            }
            if (operatorModel.Canceler != null)
            {
                result.flag = false;
                result.message = "操作员已注销，无需重复操作！";
                return Json(result);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (operatorBll.AlterStatus(operatorModel, status, user.SysOperator.ID, type) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "注销失败";
            return Json(result);
        }

        //激活
        [HttpPost]
        [Privilege(Menu = "员工管理", Name = "激活")]
        public ActionResult Active(long opid)
        {
            var result = new AjaxResult();
            int status = 0;
            int type = 0;
            var operatorModel = operatorBll.GetOperatorbyId(opid);
            if (operatorModel == null)
            {
                result.flag = false;
                result.message = "未找到操作员信息";
                return Json(result);
            }
            if (operatorModel.Canceler == null)
            {
                result.flag = false;
                result.message = "操作员已激活，无需重复操作！";
                return Json(result);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (operatorBll.AlterStatus(operatorModel, status, user.SysOperator.ID, type) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "激活失败";
            return Json(result);

        }

        [Privilege(Menu = "员工管理", Name = "重置密码")]
        public ActionResult ResetPassword(long opid, string newpwd)
        {
            var result = new AjaxResult();
            var operatorModel = operatorBll.GetOperatorbyId(opid);
            if (operatorModel == null)
            {
                result.flag = false;
                result.message = "未找到操作员信息";
                return Json(result);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (operatorBll.AlterPassword(opid, Util.GetPassword(operatorModel.LoginName, newpwd.Trim())) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "重置密码失败";
            return Json(result);

        }


        //public List<TreeNode> GetSupplierTreeNode(int orgid)
        //{
        //    List< T_BD_SUPPLIERGROUP> supplierGroups = supplierGroupBll.GetAll();
        //    List<T_BD_SUPPLIERGROUP_L> supplierGroupL = supplierGroupBll.GetAllLanguage();
        //    List<T_BD_SUPPLIER> supplier = supplierBll.GetAll();
        //    List<T_BD_SUPPLIER_L> supplierL = supplierBll.GetAllLanguage();

        //   return GetSupplierGroupTreeNode(supplierGroups, supplierGroupL, supplier, supplierL, orgid);
        //}

        //public List<TreeNode> GetSupplierGroupTreeNode(List<T_BD_SUPPLIERGROUP> supplierGroups,
        //    List<T_BD_SUPPLIERGROUP_L> supplierGroupL,
        //    List<T_BD_SUPPLIER> supplier, List<T_BD_SUPPLIER_L> supplierL, int orgid, long pid = 0 )
        //{
        //    //先取组
        //    List<TreeNode> result = (from i in supplierGroups
        //                             join iL in supplierGroupL on i.FID equals iL.FID
        //                             where i.FPARENTID == pid 
        //                             select new TreeNode() { id = i.FID, text = i.FNUMBER + "(" + iL.FNAME + ")-组"+i.FID, attributes = new { type = "SupplierGroup" } }
        //                            ).ToList();
        //    if (result.Count==0 )
        //    {
        //       return (   
        //                  from i in supplier
        //                  join iL in supplierL on i.FSUPPLIERID equals iL.FSUPPLIERID
        //                  where i.FPRIMARYGROUP == pid && i.FDOCUMENTSTATUS == "C"&& i.FUSEORGID== orgid
        //                  select new TreeNode() { id = i.FSUPPLIERID, text = i.FNUMBER+"(" +iL.FNAME +")-供应商"+i.FSUPPLIERID, attributes = new { type = "Supplier" } }
        //                 ).ToList();
        //    }
        //    foreach (var item in result)
        //    {
        //        item.children = new List<TreeNode>();
        //        item.children.AddRange(GetSupplierGroupTreeNode(supplierGroups, supplierGroupL, supplier, supplierL,orgid, item.id));
        //    }
        //    return result;

        //}
        //private ActionResult GetOrganizationTree()
        //{
        //    var org = this.orgbll.GetAll();
        //    var orgL = this.orgbll.GetAllLanguage();
        //    var result = (from i in org
        //                  join j in orgL on i.FORGID equals j.FORGID
        //                  where i.FORGID == i.FPARENTID && j.FLOCALEID == 2052
        //                  select new TreeNode() { id = i.FORGID, text = i.FNUMBER + "(" + j.FNAME + ")" + i.FORGID }).ToList();

        //    foreach (var item in result)
        //    {
        //        var node = GetOrganizationTreeNode(int.Parse(item.id.ToString()), org, orgL);
        //        if (node != null)
        //        {
        //            item.children = new List<TreeNode>();
        //            item.children.AddRange(node);
        //        }
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //private List<TreeNode> GetOrganizationTreeNode(int orgid, List<T_ORG_ORGANIZATIONS> org, List<T_ORG_ORGANIZATIONS_L> orgL)
        //{
        //    var result = (from i in org
        //                  join j in orgL on i.FORGID equals j.FORGID
        //                  where i.FPARENTID == orgid && i.FORGID != i.FPARENTID && j.FLOCALEID == 2052
        //                  select new TreeNode() { id = i.FORGID, text = i.FNUMBER + "(" + j.FNAME + ")" + i.FORGID }).ToList();

        //    if (result.Count == 0)
        //    {
        //        return null;
        //    }
        //    foreach (var item in result)
        //    {
        //        if (item.id == 1)
        //            continue;
        //        var node = GetOrganizationTreeNode(int.Parse(item.id.ToString()), org, orgL);
        //        if (node != null)
        //        {
        //            item.children = new List<TreeNode>();
        //            item.children.AddRange(node);
        //        }
        //    }


        //    return result;
        //}


    }

}