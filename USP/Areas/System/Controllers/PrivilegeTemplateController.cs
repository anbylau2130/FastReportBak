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
using USP.Service;

namespace USP.Areas.System.Controllers
{
    public class PrivilegeTemplateController : SysPrivilegeController
    {
        IDropDownListService dropDownListService;
        ISysPrivilegeTemplateBll privilegeTemplateBll;
        ISysCorpTypeBll sysCorpTypeBll;
        public PrivilegeTemplateController(IDropDownListService dropDownListService, ISysPrivilegeTemplateBll privilegeTemplateBll, ISysCorpTypeBll sysCorpTypeBll)
        {
            this.dropDownListService = dropDownListService;
            this.privilegeTemplateBll = privilegeTemplateBll;
            this.sysCorpTypeBll = sysCorpTypeBll;
        }

        [MenuItem(Parent = "系统设置", Name = "权限模板", Icon = "icon-file-alt")]
        public ActionResult PrivilegeTemplates()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrivilegeTemplates(string actionName)
        {
            return OtherAction(actionName);
        }

        [HttpPost]
        public ActionResult GetPrivilegeTemplateData()
        {
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            string corpType = Request["corpType"];
            if (corpType == null)
            {
                corpType = string.Empty;
            }
            return this.ToJson(privilegeTemplateBll.GetPrivilegeTemplateGrid(page, rows, corpType, "", ""));
        }

        [HttpGet]
        [Privilege(Menu = "权限模板", Name = "设置")]
        public ActionResult AddPrivilegeTemplate(long corptype)
        {
            var privileges = privilegeTemplateBll.GetSysPrivilegeTemplateByCorpType(corptype).Aggregate(string.Empty, (current, item) => current + (item.Privilege + ","));
            var treeList = privilegeTemplateBll.GetPrivilegeTreeList(corptype,0, privilegeTemplateBll.GetPrivileges(), privileges.TrimEnd(','));
            PrivilegeTemplateResult result = new PrivilegeTemplateResult()
            {
                Privileges = privileges,
                CorpType = corptype
            };
            ViewBag.CorpTypeName = sysCorpTypeBll.GetModelById(corptype).Name;
            ViewData["TreeList"] = new TreeOptions()
            {
                animate = false,
                cascadeCheck = false,
                checkbox = true,
                data = treeList
            };

            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            if (treeList.Count == 0)
            {
                ModelState.AddModelError("Privileges","请先设置菜单模板！");
            }

            return View(result);
        }

        [HttpPost]
        public ActionResult AddPrivilegeTemplate(PrivilegeTemplateResult privilegeTemplate)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;

            ViewData["TreeList"] = new TreeOptions()
            {
                animate = false,
                cascadeCheck = false,
                checkbox = true,
                data = privilegeTemplateBll.GetPrivilegeTreeList(privilegeTemplate.CorpType,0, privilegeTemplateBll.GetPrivileges())
            };

            if (privilegeTemplate.Privileges == null)
            {
                TempData["returnMsgType"] = "false";
                TempData["returnMsg"] = "您未选择权限！";
                return RedirectToAction("AddPrivilegeTemplate", "PrivilegeTemplate", new { area = "System", corpType = privilegeTemplate.CorpType });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = privilegeTemplateBll.SavePrivileges(privilegeTemplate.CorpType, privilegeTemplate.Privileges.TrimEnd(','), currentUser.SysOperator.ID);
                    if (!result.flag)
                    {
                        ModelState.AddModelError("Privileges", result.message);
                        return View(privilegeTemplate);
                    }
                    else
                    {
                        TempData["returnMsgType"] = "success";
                        TempData["returnMsg"] = "设置成功";
                        return RedirectToAction("PrivilegeTemplates", "PrivilegeTemplate", new { area = "System", corptype = privilegeTemplate.CorpType });
                    }
                }
                else
                {
                    TempData["returnMsgType"] = "false";
                    TempData["returnMsg"] = "设置失败！";
                    return RedirectToAction("AddPrivilegeTemplate", "PrivilegeTemplate", new { area = "System", corpType = privilegeTemplate.CorpType });
                }
            }
        }

        //[NoFilter(Flag = true)]
        public JsonResult GetPrivilegeTree()
        {
            int corpType = Convert.ToInt32(Request["corpType"]);

            if (corpType == -1)
            {
                return this.Json(new AjaxResult()
                {
                    dateTime = DateTime.Now,
                    flag = true,
                    attachment = new
                    {
                        treeData = string.Empty,
                        privilegeData = string.Empty
                    }
                });
            }
            var privilegeTemplate = privilegeTemplateBll.GetSysPrivilegeTemplateByCorpType(corpType);
            StringBuilder privilegeResult = new StringBuilder();
            foreach (var item in privilegeTemplate)
            {
                privilegeResult.Append(item.Privilege).Append(",");
            }
            var treeList = privilegeTemplateBll.GetPrivilegeTreeList(corpType,0, privilegeTemplateBll.GetPrivileges(), privilegeResult.ToString().TrimEnd(','));
            return this.Json(new AjaxResult()
            {
                dateTime = DateTime.Now,
                flag = true,
                attachment = new
                {
                    treeData = treeList,
                    privilegeData = privilegeResult.ToString().TrimEnd(',')
                }
            });
        }

        [HttpGet]
        [Privilege(Menu = "权限模板", Name = "审核")]
        public ActionResult AuditorPrivilegeTemplate(long corptype = 0)
        {
            var privileges = privilegeTemplateBll.GetSysPrivilegeTemplateByCorpType(corptype).Aggregate(string.Empty, (current, item) => current + (item.Privilege + ","));
            var treeList = privilegeTemplateBll.GetPrivilegeTreeList(corptype,0, privilegeTemplateBll.GetPrivileges(), privileges.TrimEnd(','));
            PrivilegeTemplateResult result = new PrivilegeTemplateResult()
            {
                Privileges = privileges,
                CorpType = corptype
            };
            ViewBag.CorpTypeName = sysCorpTypeBll.GetModelById(corptype).Name;
            ViewData["TreeList"] = new TreeOptions()
            {
                animate = false,
                cascadeCheck = false,
                checkbox = true,
                data = treeList
            };

            return View(result);
        }

        [HttpPost]
        public ActionResult AuditorPrivilegeTemplate(PrivilegeTemplateResult privilegeTemplate)
        {
            //处理其他请求
            var ac = Request["actionName"] ?? "";
            if (ac != "")
                return OtherAction(ac);

            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            ViewData["TreeList"] = new TreeOptions()
            {
                animate = false,
                cascadeCheck = false,
                checkbox = true,
                data = privilegeTemplateBll.GetPrivilegeTreeList(privilegeTemplate.CorpType, 0, privilegeTemplateBll.GetPrivileges())
            };
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();

            if (ModelState.IsValid)
            {
                var result = privilegeTemplateBll.AuditorPrivileges(privilegeTemplate.CorpType, privilegeTemplate.Privileges.TrimEnd(','), currentUser.SysOperator.ID);
                if (!result.flag)
                {
                    ModelState.AddModelError("Privileges", result.message);
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "审核通过失败";
                    return View(privilegeTemplate);
                }
                else
                {
                    TempData["returnMsgType"] = "success";
                    TempData["returnMsg"] = "审核通过成功";
                    return RedirectToAction("PrivilegeTemplates", "PrivilegeTemplate", new { area = "System" });
                }
            }
            TempData["CorpType"] = privilegeTemplate.CorpType;
            return View("PrivilegeTemplates");
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac.ToLower())
            {
                case "datagrid":
                    return GetPrivilegeTemplateData();
                case "privilegetree":
                    return GetPrivilegeTree();
                case "corptype":
                    return GetCorpTypeList();
                default:
                    return Content("");
            }
        }

        public ActionResult GetCorpTypeList()
        {
            return Json(sysCorpTypeBll.GetCorpTypeList(0), JsonRequestBehavior.AllowGet);
        }
    }
}