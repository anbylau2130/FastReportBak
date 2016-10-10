using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Controllers;
using USP.Bll;
using USP.Common;
using USP.Models.POCO;
using USP.Attributes;
using USP.Models.Entity;
using USP.Service;

namespace USP.Areas.System.Controllers
{
    [Menu(Name = "账号信息", Icon = "panel-icon icon-lock")]
    public class SysOperatorController : SysPrivilegeController
    {
        // GET: System/SysOperator
        ISysOperatorBll sysOperatorBll;
        ISysCorpTypeBll sysCorpTypeBll;
        ISysSkinBll sysSkinBll;
        ISysCorpBll sysCorpBll;
        public SysOperatorController(ISysOperatorBll sysOperatorBll, ISysCorpTypeBll sysCorpTypeBll, ISysSkinBll sysSkinBll, ISysCorpBll sysCorpBll)
        {
            this.sysOperatorBll = sysOperatorBll;
            this.sysCorpTypeBll = sysCorpTypeBll;
            this.sysSkinBll = sysSkinBll;
            this.sysCorpBll = sysCorpBll;
        }

        [MenuItem(Parent = "账号信息", Name = "基本信息", Icon = "glyphicon glyphicon-user")]
        [Privilege(Menu = "基本信息", Name = "编辑")]
        [HttpGet]
        public ActionResult Index()
        {
            var model = sysOperatorBll.GetModel(((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID);

            ViewBag.CorpName = sysCorpBll.GetCorporation(model.Corp).Name;
            ViewBag.Skin = sysSkinBll.Getlists();
            ViewBag.Province = model.Province;
            ViewBag.Area = model.Area;
            ViewBag.County = model.County;
            ViewBag.Community = model.Community;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SysOperator model)
        {
            ViewBag.Skin = sysSkinBll.Getlists();
            if (ModelState.IsValid)
            {
                var entity = sysOperatorBll.GetModel(model.ID);

                entity.RealName = model.RealName;
                entity.Mobile = model.Mobile;
                entity.IdCard = model.IdCard;
                entity.Email = model.Email;
                entity.Weibo = model.Weibo;
                entity.Skin = model.Skin;
                entity.Province = model.Province;
                entity.Area = model.Area;
                entity.County = model.County;
                entity.Community = model.Community;
                entity.Address = model.Address;
                entity.AlipayOpenid = model.AlipayOpenid;
                entity.WechatOpenid = model.WechatOpenid;

                var result = sysOperatorBll.Edit(entity);
                if (result.flag == true)
                {
                    TempData["returnMsgType"] = "success";
                    TempData["returnMsg"] = "编辑成功";
                }
                else
                {
                    TempData["returnMsgType"] = "error";
                    TempData["returnMsg"] = "编辑失败";
                }
                return RedirectToAction("Index", "SysOperator", new { area = "System" });
            }
            else
            {
                return View("Index", model);
            }
        }
    }
}