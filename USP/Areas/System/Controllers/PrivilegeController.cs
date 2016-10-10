using System;
using System.Web.Mvc;
using USP.Controllers;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class PrivilegeController : SysPrivilegeController
    {
        ISysPrivilegeBll sysPrivilegeBll;

        public PrivilegeController(ISysPrivilegeBll sysPrivilegeBll)
        {
            this.sysPrivilegeBll = sysPrivilegeBll;
        }

        [HttpGet]
        [MenuItem(Parent = "系统设置", Name = "权限管理", Icon = "glyphicon glyphicon-lock")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }

        [HttpGet]
        [Privilege(Menu = "权限管理", Name = "新增")]
        public ActionResult AddPrivilege()
        {
            return View(new PrivilegeAddEdit());
        }

        [HttpPost]
        public ActionResult AddPrivilege(PrivilegeAddEdit model)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                if (sysPrivilegeBll.CheckPrivilegeName(model.Name.Trim(), model.Menu))
                {
                    ModelState.AddModelError("Name", "权限已存在");
                    return View(model);
                }
                var user = Session[Constants.USER_KEY] as User;
                model.Creator = user.SysOperator.ID;

                var sysPrivilege = new SysPrivilege();
                sysPrivilege.ID = -1;
                sysPrivilege.Menu = model.Menu;
                sysPrivilege.Parent = 0;
                sysPrivilege.Name = model.Name.Trim();
                sysPrivilege.Clazz = model.Clazz.Trim();
                sysPrivilege.Area = model.Area.Trim();
                sysPrivilege.Controller = model.Controller.Trim();
                sysPrivilege.Method = model.Method.Trim();
                if (!string.IsNullOrWhiteSpace(model.Parameter))
                    sysPrivilege.Parameter = model.Parameter.Trim();
                else
                    sysPrivilege.Parameter = "";
                sysPrivilege.Url = model.Url;
                sysPrivilege.Reserve = "";
                sysPrivilege.Remark = model.Remark;
                sysPrivilege.Creator = model.Creator;
                sysPrivilege.CreateTime = DateTime.Now;
                sysPrivilege.Auditor = model.Creator;
                var result = sysPrivilegeBll.AddPrivilege(sysPrivilege);
                if (result == null)
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "新增失败";
                    //ModelState.AddModelError("errorresult", "新增失败");
                    return View(model);
                }
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "新增成功";
                return RedirectToAction("AddPrivilege", "Privilege");
            }
            return View(model);
        }

        [HttpGet]
        [Privilege(Menu = "权限管理", Name = "修改")]
        public ActionResult EditPrivilege(long id)
        {
            var sysPrivilege = sysPrivilegeBll.GetPrivilegeByID(id);
            if (sysPrivilege == null) return RedirectToAction("Index", "Privilege");
            var model = new PrivilegeAddEdit();
            model.ID = sysPrivilege.ID;
            model.Menu = sysPrivilege.Menu;
            model.Name = sysPrivilege.Name;
            model.Clazz = sysPrivilege.Clazz;
            model.Area = sysPrivilege.Area;
            model.Controller = sysPrivilege.Controller;
            model.Method = sysPrivilege.Method;
            model.Parameter = sysPrivilege.Parameter;
            model.Url = sysPrivilege.Url;
            model.Remark = sysPrivilege.Remark;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPrivilege(PrivilegeAddEdit model)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                var sysPrivilege = sysPrivilegeBll.GetPrivilegeByID(model.ID);
                var user = Session[Constants.USER_KEY] as User;
                if (model.Name.Trim() != sysPrivilege.Name.Trim())
                {
                    if (sysPrivilegeBll.CheckPrivilegeName(model.Name.Trim(), model.Menu))
                    {
                        ModelState.AddModelError("Name", "权限已存在");
                        return View(model);
                    }
                }

                sysPrivilege.Menu = model.Menu;
                sysPrivilege.Name = model.Name.Trim();
                sysPrivilege.Clazz = model.Clazz.Trim();
                sysPrivilege.Area = model.Area.Trim();
                sysPrivilege.Controller = model.Controller.Trim();
                sysPrivilege.Method = model.Method.Trim();
                if (!string.IsNullOrWhiteSpace(model.Parameter))
                    sysPrivilege.Parameter = model.Parameter.Trim();
                else
                    sysPrivilege.Parameter = "";
                sysPrivilege.Url = model.Url;
                sysPrivilege.Reserve = "";
                sysPrivilege.Remark = model.Remark;
                sysPrivilege.Creator = user.SysOperator.ID; ;
                sysPrivilege.CreateTime = DateTime.Now;
                var result = sysPrivilegeBll.EditPrivilege(sysPrivilege);
                if (result == null)
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "修改失败";
                    //ModelState.AddModelError("errorresult", "修改失败");
                    return View(model);
                }
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "修改成功";
                return RedirectToAction("Index", "Privilege");
            }
            return View(model);
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return GetDataGrid();
                case "menucombotree":
                    return GetMenuComboTree();//列表页菜单combotree
                case "menutree"://加载菜单tree
                    return GetMenuTree();
                case "checkname"://添加页检测权限名
                    return CheckName();
                default:
                    return Content("");
            }
        }

        private ActionResult GetDataGrid()
        {
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
            long? menuId = null;
            long temp;
            if (long.TryParse(Request["menuId"], out temp))
            {
                menuId = temp;
            }
            if (menuId == -1)
            {
                menuId = null;//-1表示全部
            }
            var result = sysPrivilegeBll.SearchSysPrivilegePage(page, rows, menuId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetMenuComboTree()
        {
            var comboTree = sysPrivilegeBll.GetMenuTree(0, null);
            comboTree.Insert(0, new TreeNode
            {
                id = -1,
                text = "==全部=="
            });//添加==全部==项
            return Json(comboTree, JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetMenuTree()
        {
            var strMenu = Request["menu"];
            long? menu = null;
            long temp;
            if (long.TryParse(strMenu, out temp))
            {
                menu = temp;
            }
            var comboTree = sysPrivilegeBll.GetMenuTree(0, 3);
            comboTree.Insert(0, new TreeNode
            {
                text = "==请选择=="
            });//添加==请选择==项
            return Json(comboTree, JsonRequestBehavior.AllowGet);
        }


        private ActionResult CheckName()
        {
            var resutl = new AjaxResult();
            var name = Request["name"];
            if (string.IsNullOrWhiteSpace(name))
            {
                resutl.flag = false;
                resutl.message = "请输入权限名称";
                return Json(resutl);
            }

            var strmenu = Request["menu"];
            long menu;
            if (!long.TryParse(strmenu, out menu))
            {
                resutl.flag = false;
                resutl.message = "请先选择菜单";
                return Json(resutl);
            }
            var strid = Request["privilegeId"];
            long? privilegeId = null;
            long temp;
            if (long.TryParse(strid, out temp))
            {
                privilegeId = temp;
            }
            if (privilegeId == null)
            {
                if (sysPrivilegeBll.CheckPrivilegeName(name.Trim(), menu))
                {
                    resutl.flag = false;
                    resutl.message = "权限已存在";
                    return Json(resutl);
                }
                resutl.flag = true;
                resutl.message = "";
                return Json(resutl);
            }
            var model = sysPrivilegeBll.GetPrivilegeByID((long)privilegeId);
            if (model.Name.Trim() != name.Trim())
            {
                if (sysPrivilegeBll.CheckPrivilegeName(name.Trim(), menu))
                {
                    resutl.flag = false;
                    resutl.message = "权限已存在";
                    return Json(resutl);
                }
            }
            resutl.flag = true;
            resutl.message = "";
            return Json(resutl);
        }

        [HttpPost]
        [Privilege(Menu = "权限管理", Name = "注销")]
        public ActionResult CancelPrivilege(long privilege)
        {
            var resutl = new AjaxResult();
            var privilegeModel = sysPrivilegeBll.GetPrivilegeByID(privilege);
            if (privilegeModel == null)
            {
                resutl.flag = false;
                resutl.message = "未找到权限信息";
                return Json(resutl);
            }
            if (privilegeModel.Canceler != null)
            {
                resutl.flag = false;
                resutl.message = "权限已注销，无需重复操作！";
                return Json(resutl);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (sysPrivilegeBll.DeletePrivilege(privilegeModel.ID, user.SysOperator.ID))
            {
                resutl.flag = true;
                return Json(resutl);
            }
            resutl.flag = false;
            resutl.message = "注销失败";
            return Json(resutl);
        }

        [HttpPost]
        [Privilege(Menu = "权限管理", Name = "激活")]
        public ActionResult ActivePrivilege(long privilege)
        {
            var resutl = new AjaxResult();
            var privilegeModel = sysPrivilegeBll.GetPrivilegeByID(privilege);
            if (privilegeModel == null)
            {
                resutl.flag = false;
                resutl.message = "未找到权限信息";
                return Json(resutl);
            }
            if (privilegeModel.Canceler == null)
            {
                resutl.flag = false;
                resutl.message = "权限已激活，无需重复操作！";
                return Json(resutl);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (sysPrivilegeBll.ActionPrivilege(privilegeModel.ID, user.SysOperator.ID))
            {
                resutl.flag = true;
                return Json(resutl);
            }
            resutl.flag = false;
            resutl.message = "激活失败";
            return Json(resutl);
        }
    }
}