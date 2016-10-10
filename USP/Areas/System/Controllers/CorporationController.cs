using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using USP.Attributes;
using USP.Bll;
using USP.Common;
using USP.Controllers;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using System.Text;
using USP.Bll.Impl;
using USP.Utility;

namespace USP.Areas.System.Controllers
{
    [Menu(Name = "公司管理", Icon = "icon-building")]
    public class CorporationController : SysPrivilegeController
    {
        ISysCorpBll sysCorpBll;
        //ISysCorpTypeBll sysCorpTypeBll;
        IDropDownListService dropDownListService;
        ISysCorpStatusBll sysCorpStatusBll;
        //ISupplierBll supplierBll;

        public CorporationController(ISysCorpBll sysCorpBll, IDropDownListService dropDownListService, ISysCorpStatusBll sysCorpStatusBll /*, ISupplierBll supplierBll*/)
        {
            this.sysCorpBll = sysCorpBll;
            //this.sysCorpTypeBll = sysCorpTypeBll;
            this.dropDownListService = dropDownListService;
            this.sysCorpStatusBll = sysCorpStatusBll;
            //this.supplierBll = supplierBll;
        }

        [MenuItem(Parent = "公司管理", Name = "公司列表", Icon = "icon-sitemap")]
        public ActionResult Corporations()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Corporations(string actionName)
        {
            switch (actionName.ToLower())
            {
                case "datagrid":
                    return GetCorporationData();
                case "combobox":
                    return GetSysCorpStatus();
                default:
                    return Content("");
            }
        }

        [HttpPost]
        public ActionResult GetCorporationData()
        {
           // var temp= supplierBll.GetAll();
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            string name = Request["name"];//公司名称查询
            long status = Convert.ToInt64(Request["status"]);
            long corp = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysCorp.ID;

            StringBuilder whereStr = new StringBuilder();
            whereStr.AppendFormat("   a.Parent={0}", corp);
            if (!string.IsNullOrEmpty(name))
            {
                whereStr.AppendFormat(" and a.Name like '%{0}%'", name);
            }
            if (status != -1)
            {
                whereStr.AppendFormat(" and a.Status={0}", status);
            }
            //else
            //{
            //    whereStr.AppendFormat(" and Status={0}", status);
            //}
            return this.ToJson(sysCorpBll.GetCorpGrid(page, rows, whereStr.ToString(), "", ""));
        }

        [Privilege(Menu = "公司列表", Name = "新增")]
        public ActionResult AddCorporation()
        {
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList().Where(x => x.Text != "区域商户").ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddCorporation(CorpResult corp)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            if (ModelState.IsValid)
            {
                AjaxResult result = sysCorpBll.AddCorporation(corp.CorpName, corp.CorpType, currentUser.SysOperator.ID, currentUser.SysCorp.ID, corp.AdminLoginName, Utility.Util.GetPassword(corp.AdminLoginName, corp.AdminPassword));
                if (result.flag)
                {
                    TempData["returnMsgType"] = "success";
                    TempData["returnMsg"] = "添加成功";
                    return View("Corporations");
                }
                else
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = result.message;
                }

            }

            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList().Where(x => x.Text != "区域商户").ToList();
            return View(corp);
        }

        [Privilege(Menu = "公司列表", Name = "修改")]
        public ActionResult EditCorporation(string id)
        {
            long idParse;
            SysCorp model = new SysCorp();
            if (long.TryParse(id, out idParse))
            {
                model = sysCorpBll.GetCorporation(idParse);
            }
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            ViewData["FeeTypeList"] = dropDownListService.InitCorpFeeTypeDropDownList();
            ViewData["GradeList"] = dropDownListService.InitSysCorpGradeDropDownList();
            ViewData["VocationList"] = dropDownListService.InitVocationDropDownList();
            ViewData["ProvinceList"] = dropDownListService.InitProvinceDropDownList();
            ViewData["AreaList"] = dropDownListService.InitCityDropDownList(model.Province);
            ViewData["CountyList"] = dropDownListService.InitCountyDropDownList(model.Area);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCorporation(SysCorp corp)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            corp.Creator = currentUser.SysOperator.ID;
            corp.CreateTime = DateTime.Now;
            corp.Auditor = null;
            corp.AuditTime = null;
            if (ModelState.IsValid)
            {
                if (sysCorpBll.EditCorporation(corp))
                {
                    TempData["isSuccess"] = "true";
                    TempData["MessageInfo"] = "完善信息成功!";
                    return View("Corporations");
                }
            }
            TempData["isSuccess"] = "false";
            TempData["MessageInfo"] = "完善信息失败!";
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            ViewData["FeeTypeList"] = dropDownListService.InitCorpFeeTypeDropDownList();
            ViewData["GradeList"] = dropDownListService.InitSysCorpGradeDropDownList();
            ViewData["VocationList"] = dropDownListService.InitVocationDropDownList();
            ViewData["ProvinceList"] = dropDownListService.InitProvinceDropDownList();
            ViewData["AreaList"] = dropDownListService.InitCityDropDownList(corp.Province);
            ViewData["CountyList"] = dropDownListService.InitCountyDropDownList(corp.Area);
            return View(corp);
        }


        [MenuItem(Parent = "公司管理", Name = "公司信息", Icon = "icon-home")]
        [Privilege(Menu = "公司信息", Name = "修改")]
        public ActionResult Information()
        {
          
            long corp = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysCorp.ID;
            var  model = sysCorpBll.GetCorporation(corp);
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            ViewData["FeeTypeList"] = dropDownListService.InitCorpFeeTypeDropDownList();
            ViewData["GradeList"] = dropDownListService.InitSysCorpGradeDropDownList();
            ViewData["VocationList"] = dropDownListService.InitVocationDropDownList();
            ViewData["ProvinceList"] = dropDownListService.InitProvinceDropDownList();
            ViewData["AreaList"] = dropDownListService.InitCityDropDownList(model.Province);
            ViewData["CountyList"] = dropDownListService.InitCountyDropDownList(model.Area);
            return View(model);
        }

        [HttpPost]
        public ActionResult Information(SysCorp corp)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            corp.Creator = currentUser.SysOperator.ID;
            corp.CreateTime = DateTime.Now;
            corp.Auditor = null;
            corp.AuditTime = null;
            if (ModelState.IsValid)
            {
                if (sysCorpBll.EditCorporation(corp))
                {
                    TempData["isSuccess"] = "true";
                    TempData["MessageInfo"] = "完善信息成功!";
                    return View("Corporations");
                }
            }
            TempData["isSuccess"] = "false";
            TempData["MessageInfo"] = "完善信息失败!";
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            ViewData["FeeTypeList"] = dropDownListService.InitCorpFeeTypeDropDownList();
            ViewData["GradeList"] = dropDownListService.InitSysCorpGradeDropDownList();
            ViewData["VocationList"] = dropDownListService.InitVocationDropDownList();
            ViewData["ProvinceList"] = dropDownListService.InitProvinceDropDownList();
            ViewData["AreaList"] = dropDownListService.InitCityDropDownList(corp.Province);
            ViewData["CountyList"] = dropDownListService.InitCountyDropDownList(corp.Area);
            return View(corp);

        }

        [HttpPost]
        [Privilege(Menu = "公司列表", Name = "注销")]
        public JsonResult CancelCorporation(long id)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            return Json(sysCorpBll.CancelCorpotation(id, currentUser.SysOperator.ID));
        }

        [HttpPost]
        [Privilege(Menu = "公司列表", Name = "激活")]
        public JsonResult EnableCorporation(long id)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;
            return Json(sysCorpBll.EnableCorpotation(id, currentUser.SysOperator.ID));
        }

        [Privilege(Menu = "公司列表", Name = "审核")]
        [HttpGet]
        public ActionResult AuditorCorporation(string id)
        {
            long idParse;
            SysCorp model = new SysCorp();
            if (long.TryParse(id, out idParse))
            {
                model = sysCorpBll.GetCorporation(idParse);
            }
            ViewData["CorpTypeList"] = dropDownListService.InitCorpTypeDropDownList();
            ViewData["FeeTypeList"] = dropDownListService.InitCorpFeeTypeDropDownList();
            ViewData["GradeList"] = dropDownListService.InitSysCorpGradeDropDownList();
            ViewData["VocationList"] = dropDownListService.InitVocationDropDownList();
            ViewBag.Province = model.Province;
            ViewBag.Area = model.Area;
            ViewBag.County = model.County;
            ViewBag.Community = model.Community;
            return View(model);
        }

        [HttpPost]
        public ActionResult AuditorCorporation(SysCorp corp)
        {
            var currentUser = HttpContext.Session[Constants.USER_KEY] as USP.Models.POCO.User;

            var result = sysCorpBll.AuditorCorporation(corp.ID, currentUser.SysOperator.ID);
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

            return RedirectToAction("Corporations", "Corporation", new { area = "System" });
        }

        public ActionResult GetSysCorpStatus()
        {
            return Json(sysCorpStatusBll.GetSysCorpStatus(), JsonRequestBehavior.AllowGet);
        }

        [NoFilter(Flag = true)]
        public JsonResult GetAreaDropDown(string provinceCode)
        {
            List<object> result = new List<object>();
            foreach (SelectListItem item in dropDownListService.InitCityDropDownList(provinceCode))
            {
                result.Add(new { id = item.Value, text = item.Text });
            }
            return Json(result);
        }

        [NoFilter(Flag = true)]
        public JsonResult GetCountyDropDown(string areaCode)
        {
            List<object> result = new List<object>();
            foreach (SelectListItem item in dropDownListService.InitCountyDropDownList(areaCode))
            {
                result.Add(new { id = item.Value, text = item.Text });
            }
            return Json(result);
        }
    }
}