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
    public class SysMenuTemplateController : SysPrivilegeController
    {
        ISysMenuTemplateBll sysMenuTemplateBll;
        ISysMenuBll sysMenuBll;
        ISysCorpTypeBll sysCorpTypeBll;

        public SysMenuTemplateController(ISysMenuTemplateBll sysMenuTemplateBll, ISysMenuBll sysMenuBll, ISysCorpTypeBll sysCorpTypeBll)
        {
            this.sysMenuTemplateBll = sysMenuTemplateBll;
            this.sysMenuBll = sysMenuBll;
            this.sysCorpTypeBll = sysCorpTypeBll;
        }

        [MenuItem(Parent = "系统设置", Name = "菜单模板", Icon = "icon-file-alt")]
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


        public ActionResult GetCorpTypes()
        {
            return Json(sysCorpTypeBll.GetCorpTypeList(0), JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "菜单模板", Name = "设置")]
        [HttpGet]
        public ActionResult Add(int corpType)
        {
            ViewBag.CorpType = corpType;
            ViewBag.CorpTypeName = sysCorpTypeBll.GetModelById(corpType).Name;
            return View();
        }

        [HttpPost]
        public ActionResult Add(string menu, int corpType = 0)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);


            long creator = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;

            if (string.IsNullOrEmpty(menu))
            {
                TempData["returnMsgType"] = "false";
                TempData["returnMsg"] = "您未选择菜单！";
                return RedirectToAction("Add", "SysMenuTemplate", new { area = "System", corpType = corpType });
            }
            else
            {
                var result = sysMenuTemplateBll.Edit(corpType, creator, menu);
                if (result.flag == true)
                {
                    TempData["returnMsgType"] = "success";
                    TempData["returnMsg"] = "设置成功";

                }
                else
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "设置失败";
                    return View("Add");
                }
                return RedirectToAction("Index", "SysMenuTemplate", new { area = "System", corpType= corpType });
            }
        }

        [HttpGet]
        [Privilege(Menu = "菜单模板", Name = "审核")]
        public ActionResult Audit(int corpType)
        {
            ViewBag.CorpType = corpType;
            ViewBag.CorpTypeName = sysCorpTypeBll.GetModelById(corpType).Name;
            return View();
        }

        [HttpPost]
        public ActionResult Audit(string menu, int corpType = 0)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);
            long operater = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;
            var result = sysMenuTemplateBll.Audit(corpType, operater, menu);
            if (result.flag == true)
            {
                TempData["returnMsgType"] = "success";
                TempData["returnMsg"] = "审核成功";
            }
            else
            {
                TempData["returnMsgType"] = "error";
                TempData["returnMsg"] = "审核失败";
            }
            return RedirectToAction("Index", "SysMenuTemplate", new { area = "System",corpType= corpType });
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return GetDataGrid();
                case "menutree":
                    return GetCorpTypeMenu();
                case "corptype":
                    return GetCorpTypes();
                default:
                    return Content("");
            }
        }

        private ActionResult GetDataGrid()
        {
            string currentPage = Request["page"];
            string pagesize = Request["rows"];
            int id = Convert.ToInt32(Request["corptype"]);

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

            var entitys = sysMenuTemplateBll.getSysMenuTempGrid(id);
            List<UP_GetMenuTemplate_Result> list = entitys.Skip((page - 1) * size).Take(size).ToList();
            int count = entitys.Count();

            DataGrid<UP_GetMenuTemplate_Result> result = new DataGrid<UP_GetMenuTemplate_Result>();
            result.rows = list;
            result.total = count;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ActionResult GetCorpTypeMenu()
        {
            var corpType = Convert.ToInt32(Request["corpType"]);

            var user = Session[Constants.USER_KEY] as User;
            var tree = sysMenuTemplateBll.GetUserRoleMenuPrivilegeTree(corpType);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }
    }
}