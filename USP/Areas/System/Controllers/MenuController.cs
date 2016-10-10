using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Controllers;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    public class MenuController : SysPrivilegeController
    {
        ISysMenuBll sysMenuBll;
        ISysPrivilegeBll sysPrivilegeBll;
        public MenuController(ISysMenuBll sysMenuBll, ISysPrivilegeBll sysPrivilegeBll)
        {
            this.sysMenuBll = sysMenuBll;
            this.sysPrivilegeBll = sysPrivilegeBll;
        }

        [MenuItem(Parent = "系统设置", Name = "菜单管理", Icon = "glyphicon glyphicon-list")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string actionName)
        {
            return OtherAction(actionName);
        }

        [Privilege(Menu = "菜单管理", Name = "添加")]
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Id = 0;
            ViewBag.BtnTitle = "添加";
            ViewBag.PanelTitle = "添加菜单";
            return View(new Models.Entity.SysMenu());
        }

        [Privilege(Menu = "菜单管理", Name = "编辑")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            ViewBag.BtnTitle = "编辑";
            ViewBag.PanelTitle = "编辑菜单";
            var entity = sysMenuBll.GetModel(Convert.ToInt32(id));
            return View("Add", entity);
        }

        [HttpPost]
        public ActionResult Add(SysMenu model)
        {
            ////处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                model.Creator = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;

                if (model.ID == 0)
                {
                    var result = sysMenuBll.Add(model);
                    if (result.flag == true)
                    {
                        TempData["returnMsgType"] = "success";
                        TempData["returnMsg"] = "添加成功";
                        return RedirectToAction("Index", "Menu", new { area = "System" });
                    }
                    else
                    {
                        TempData["returnMsgType"] = "error";
                        TempData["returnMsg"] = "添加失败";
                        return View("Add");
                    }
                }
                else
                {
                    var result = sysMenuBll.Edit(model);
                    if (result.flag == true)
                    {
                        TempData["returnMsgType"] = "success";
                        TempData["returnMsg"] = "编辑成功";
                        return RedirectToAction("Index", "Menu", new { area = "System" });
                    }
                    else
                    {
                        TempData["returnMsgType"] = "error";
                        TempData["returnMsg"] = "编辑失败";
                        return View("Add", model);
                    }
                }
            }
            else
            {
                TempData["returnMsgType"] = "error";
                if (model.ID == 0)
                {
                    TempData["returnMsg"] = "添加失败";
                    return View("Add");
                }
                else
                {
                    TempData["returnMsg"] = "修改失败";
                    return View("Add", model);
                }
            }
        }

        [Privilege(Menu = "菜单管理", Name = "审核")]
        [HttpGet]
        public ActionResult Audit(int id)
        {
            var entity = sysMenuBll.GetModel(Convert.ToInt32(id));
            ViewBag.MenuName = sysMenuBll.GetModel(entity.Parent).Name;
            return View(entity);
        }

        [HttpPost]
        public ActionResult Audit(Models.Entity.SysMenu model)
        {

            model.Auditor = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;
            var result = sysMenuBll.Audit(model);
            if (result.flag == true)
            {
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "审核通过成功";
            }
            else
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = "审核通过失败";
            }

            return RedirectToAction("Index");
        }

        [Privilege(Menu = "菜单管理", Name = "删除")]
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            long auditor = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;

            return Json(sysMenuBll.Cancel(id, auditor), JsonRequestBehavior.AllowGet);
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return Data();
                case "menutree":
                    return GetMenuTree();
                case "checkname":
                    return IsExistMenuName();
                default:
                    return Content("");
            }
        }

        private ActionResult Data()
        {
            string currentPage = Request["page"];
            string pagesize = Request["rows"];
            string name = Request["name"];

            int page = 1;
            int size = 10;

            if (!string.IsNullOrEmpty(currentPage))
            {
                page = int.Parse(currentPage);
            }
            if (!string.IsNullOrEmpty(pagesize))
            {
                size = int.Parse(pagesize);
            }


            var entitys = sysMenuBll.getMenuGrid(name);
            List<SysMenu> list = entitys.Skip((page - 1) * size).Take(size).ToList();
            int count = entitys.Count();

            DataGrid<SysMenu> result = new DataGrid<SysMenu>();
            result.rows = list;
            result.total = count;
            return Json(result, JsonRequestBehavior.AllowGet);
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

            var comboTree = sysPrivilegeBll.GetMenuTree(0, menu);
            comboTree.Insert(0, new TreeNode
            {
                text = "==请选择=="
            });//添加==请选择==项
            return Json(comboTree, JsonRequestBehavior.AllowGet);
        }

        private ActionResult IsExistMenuName()
        {
            int id = Convert.ToInt32(Request["id"]);
            int parent = Convert.ToInt32(Request["parent"]);
            string name = Request["name"];

            return Json(sysMenuBll.IsExistMenuName(id, parent, name), JsonRequestBehavior.AllowGet);
        }
    }
}