using System;
using System.Web.Mvc;
using USP.Controllers;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : SysPrivilegeController
    {
        ISysRoleBll sysRoleBll;

        public RoleController(ISysRoleBll sysRoleBll)
        {
            this.sysRoleBll = sysRoleBll;
        }

        [HttpGet]
        [MenuItem(Name = "角色管理", Parent = "系统设置", Icon = "icon-eye-open")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }

        [HttpPost]
        [Privilege(Menu = "角色管理", Name = "注销")]
        public ActionResult CancelRole(long role)
        {
            var resutl = new AjaxResult();
            var roleModel = sysRoleBll.GetRoleByID(role);
            if (roleModel == null)
            {
                resutl.flag = false;
                resutl.message = "未找到角色信息";
                return Json(resutl);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (roleModel.Corp != user.SysCorp.ID || (roleModel.Type && roleModel.Corp == user.SysCorp.ID))
            {
                resutl.flag = false;
                resutl.message = "您无权操作该角色信息";
                return Json(resutl);
            }
            if (roleModel.Canceler != null)
            {
                resutl.flag = false;
                resutl.message = "角色已注销，无需重复操作！";
                return Json(resutl);
            }
            if (sysRoleBll.CancelOrActionRole(roleModel.ID, user.SysOperator.ID))
            {
                resutl.flag = true;
                return Json(resutl);
            }
            resutl.flag = false;
            resutl.message = "注销失败";
            return Json(resutl);
        }

        [HttpPost]
        [Privilege(Menu = "角色管理", Name = "激活")]
        public ActionResult ActiveRole(long role)
        {
            var resutl = new AjaxResult();
            var roleModel = sysRoleBll.GetRoleByID(role);
            if (roleModel == null)
            {
                resutl.flag = false;
                resutl.message = "未找到角色信息";
                return Json(resutl);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (roleModel.Corp != user.SysCorp.ID || (roleModel.Type && roleModel.Corp == user.SysCorp.ID))
            {
                resutl.flag = false;
                resutl.message = "您无权操作该角色信息";
                return Json(resutl);
            }
            if (roleModel.Canceler == null)
            {
                resutl.flag = false;
                resutl.message = "角色已激活，无需重复操作！";
                return Json(resutl);
            }
            if (sysRoleBll.CancelOrActionRole(roleModel.ID, user.SysOperator.ID))
            {
                resutl.flag = true;
                return Json(resutl);
            }
            resutl.flag = false;
            resutl.message = "激活失败";
            return Json(resutl);
        }

        [HttpGet]
        [Privilege(Menu = "角色管理", Name = "新增")]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RoleAddEdit model)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                var user = Session[Constants.USER_KEY] as User;
                if (sysRoleBll.CheckRoleName(model.Name.Trim(), user.SysCorp.ID))
                {
                    ModelState.AddModelError("Name", "角色名已存在");
                    return View();
                }
                model.Type = false;
                model.Corp = user.SysCorp.ID;
                model.Creator = user.SysOperator.ID;
                if (!sysRoleBll.AddRole(model))
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "新增失败";
                    //ModelState.AddModelError("errorresult", "新增失败");
                    return View(model);
                }
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "新增成功";
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        [HttpGet]
        [Privilege(Menu = "角色管理", Name = "修改")]
        public ActionResult EditRole(long id)
        {
            var role = sysRoleBll.GetRoleByID(id);
            if (role == null) return RedirectToAction("Index", "Role");

            var user = Session[Constants.USER_KEY] as User;
            if (role.Type && role.Corp == user.SysCorp.ID)
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = "您没有操作权限";
                return RedirectToAction("Index", "Role");//不能修改自身公司的管理员角色
            }

            var model = new RoleAddEdit
            {
                ID = role.ID,
                Name = role.Name,
                Remark = role.Remark
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(RoleAddEdit model)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                var role = sysRoleBll.GetRoleByID(model.ID);
                var user = Session[Constants.USER_KEY] as User;
                if (role.Type && role.Corp == user.SysCorp.ID)
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "您没有操作权限";
                    return RedirectToAction("Index", "Role");//不能修改自身公司的管理员角色
                }
                if (role.Name.Trim() != model.Name.Trim())
                {
                    if (sysRoleBll.CheckRoleName(model.Name.Trim(), role.Corp))
                    {
                        ModelState.AddModelError("Name", "角色名已存在");
                        return View(model);
                    }
                }
                if (!sysRoleBll.EditRole(model))
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "修改失败";
                    //ModelState.AddModelError("errorresult", "修改失败");
                    return View(model);
                }
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "修改成功";
                return RedirectToAction("Index", "Role");
            }
            return View(model);
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return GetDataGrid();
                case "menutree"://添加页获取登录用户所有菜单和权限
                    return GetRoleMenuPrivilege();
                case "checkname"://添加页检测角色名
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
            var roleName = Request["roleName"];
            var user = Session[Constants.USER_KEY] as User;
            var result = sysRoleBll.GetSysRolePageByCorp(page, rows, user.SysCorp.ID, roleName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetRoleMenuPrivilege()
        {
            var strRole = Request["role"];
            long? role = null;
            long temp;
            if (long.TryParse(strRole, out temp))
            {
                role = temp;
            }
            var user = Session[Constants.USER_KEY] as User;
            var tree = sysRoleBll.GetUserRoleMenuPrivilegeTree(user.SysOperator.ID, role);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        private ActionResult CheckName()
        {
            var name = Request["name"];
            var strRole = Request["role"];
            long? role = null;
            long temp;
            if (long.TryParse(strRole, out temp))
            {
                role = temp;
            }
            var resutl = new AjaxResult();
            if (string.IsNullOrWhiteSpace(name))
            {
                resutl.flag = false;
                resutl.message = "请输入角色名称";
                return Json(resutl);
            }
            var user = Session[Constants.USER_KEY] as User;
            if (role == null)
            {
                if (sysRoleBll.CheckRoleName(name.Trim(), user.SysCorp.ID))
                {
                    resutl.flag = false;
                    resutl.message = "角色名已存在";
                    return Json(resutl);
                }
                resutl.flag = true;
                resutl.message = "";
                return Json(resutl);
            }
            var roleModel = sysRoleBll.GetRoleByID((long)role);
            if (roleModel.Name.Trim() != name.Trim())
            {
                if (sysRoleBll.CheckRoleName(name.Trim(), roleModel.Corp))
                {
                    resutl.flag = false;
                    resutl.message = "角色名已存在";
                    return Json(resutl);
                }
            }
            resutl.flag = true;
            resutl.message = "";
            return Json(resutl);
        }
    }
}