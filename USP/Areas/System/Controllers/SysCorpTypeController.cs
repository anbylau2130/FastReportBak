using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SysCorpTypeController : SysPrivilegeController
    {
        ISysCorpTypeBll sysCorpTypeBll;
        public SysCorpTypeController(ISysCorpTypeBll sysCorpTypeBll)
        {
            this.sysCorpTypeBll = sysCorpTypeBll;
        }

        [MenuItem(Parent = "公司管理", Name = "公司类型", Icon = "icon-leaf")]
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

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return Data();
                case "checkname":
                    return IsExistName();
                default:
                    return Content("");
            }
        }

        public ActionResult Data()
        {
            string currentPage = Request["page"];
            string pagesize = Request["rows"];
            string name = Request["name"];
            var user = Session[Constants.USER_KEY] as User;
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
            var entitys = sysCorpTypeBll.GetSysCorpTypes(name);
            List<SysCorpType> list = entitys.Skip((page - 1) * size).Take(size).ToList();
            int count = entitys.Count();

            DataGrid<SysCorpType> result = new DataGrid<SysCorpType>();
            result.rows = list;
            result.total = count;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsExistName()
        {
            int id = Convert.ToInt32(Request["id"]);
            string name = Request["name"];
            return Json(sysCorpTypeBll.IsExisName(id, name), JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "公司类型", Name = "添加")]
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ID = 0;
            ViewBag.PanelTitle = "新增公司类型";
            ViewBag.BtnTitle = "新增";
            return View(new SysCorpType());
        }

        [Privilege(Menu = "公司类型", Name = "编辑")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ID = id;
            ViewBag.PanelTitle = "编辑公司类型";
            ViewBag.BtnTitle = "编辑";
            var entity = sysCorpTypeBll.GetModelById(id);
            return View("Add", entity);
        }

        [HttpPost]
        public ActionResult Add(SysCorpType model)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            if (ModelState.IsValid)
            {
                long @operator = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;
                if (model.ID == 0)
                {
                    model.Creator = @operator;
                    model.CreateTime = DateTime.Now;
                    if (sysCorpTypeBll.Add(model))
                    {
                        TempData["returnMsgType"] = "success";
                        TempData["returnMsg"] = "添加成功！";
                        return RedirectToAction("Index", "SysCorpType", new { area = "System" });
                    }
                    else
                    {
                        TempData["returnMsgType"] = "error";
                        TempData["returnMsg"] = "添加失败！";
                        return View("Add");
                    }
                }
                else
                {
                    var entity = sysCorpTypeBll.GetModelById(model.ID);
                    entity.Name = model.Name;
                    entity.Remark = model.Remark;
                    entity.Creator = @operator;
                    entity.CreateTime = DateTime.Now;
                    if (sysCorpTypeBll.Edit(entity))
                    {
                        TempData["returnMsgType"] = "success";
                        TempData["returnMsg"] = "编辑成功！";
                        return RedirectToAction("Index", "SysCorpType", new { area = "System" });
                    }
                    else
                    {
                        TempData["returnMsgType"] = "error";
                        TempData["returnMsg"] = "编辑失败！";
                        return View("Add", model);
                    }
                }
            }
            else
            {
                TempData["returnMsgType"] = "error";
                if (model.ID == 0)
                {
                    TempData["returnMsg"] = "添加失败！";
                    return View("Add");
                }
                else
                {
                    TempData["returnMsg"] = "修改失败！";
                    return View("Add", model);
                }
            }
        }

        [Privilege(Menu = "公司类型", Name = "注销")]
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            return Json(sysCorpTypeBll.Cancel(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }

        [Privilege(Menu = "公司类型", Name = "激活")]
        [HttpPost]
        public ActionResult Active(int id)
        {
            return Json(sysCorpTypeBll.Active(id, ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID));
        }
    }
}