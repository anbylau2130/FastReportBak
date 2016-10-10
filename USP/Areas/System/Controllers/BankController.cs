using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Controllers;
using USP.Models.POCO;
using USP.Models.Entity;
using USP.Common;

namespace USP.Areas.System.Controllers
{
    public class BankController : SysPrivilegeController
    {
        // GET: System/Bank

        ISysBankBll SysBankBll;

        public BankController(ISysBankBll SysBankBll)
        {
            this.SysBankBll = SysBankBll;
        }

        [MenuItem(Parent = "系统设置", Name = "银行管理", Icon = "glyphicon glyphicon-usd")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int page, int rows, string name, long type, string item)
        {
            var result = new DataGrid<Bank_Extend>();
            long banktype = type;
            string bankname = null;
            string nicename = null;
            if (item == "BankName")
            {
                bankname = name;
            }
            else if (item == "NiceName")
            {
                nicename = name;
            }
            result = SysBankBll.GetBankGrid(page, rows, "", "", bankname, banktype, nicename);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Privilege(Menu = "银行管理", Name = "新增")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BankAddEdit bank)
        {
            if (ModelState.IsValid)
            {
                var user = Session[Constants.USER_KEY] as User;
                bank.Creator = user.SysOperator.ID;
                //bank.Url = "~/";
                if (bank.Type == 0)
                {
                    ModelState.AddModelError("Type", "请选择银行卡类型");
                    return View();
                }
                //if (SysBankBll.GetBankbyName(bank.Name) != null)
                //{
                //    ModelState.AddModelError("Name", "银行名称已存在");
                //    return View();
                //}

                //上传图片
                HttpPostedFileBase file = Request.Files["Url"];
                if (file.FileName != "")
                {
                    try
                    {
                        var filename =
                        Path.Combine(Request.MapPath("~/Upload"), file.FileName); file.SaveAs(filename);
                        //bank.Url = filename;
                        bank.Url = "/Upload/" + file.FileName;

                        //新增银行信息
                        if (!SysBankBll.SaveBank(bank))
                        {
                            ModelState.AddModelError("errorresult", "新增失败");
                            return View();
                        }
                        TempData["resultMsgType"] = "success";
                        TempData["resultMsg"] = "新增成功";
                        return RedirectToAction("Index");

                    }
                    catch (Exception ex)
                    {
                        return Content(string.Format("上传文件出现异常：{0}", ex.Message));
                    }
                }

                else
                {
                    ModelState.AddModelError("Url", "请选择图标");
                    return View(bank);
                }
            }
            return View(bank);
        }

        [Privilege(Menu = "银行管理", Name = "激活")]
        public ActionResult Active(long bankid)
        {
            var result = new AjaxResult();
            int status = 0;
            int type = 0;
            var user = Session[Constants.USER_KEY] as User;
            if (SysBankBll.AlterStatus(bankid, status, user.SysOperator.ID, type) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "激活失败";
            return Json(result);
        }

        [Privilege(Menu = "银行管理", Name = "注销")]
        public ActionResult Cancle(long bankid)
        {
            var result = new AjaxResult();
            int status = 1;
            int type = 1;
            var user = Session[Constants.USER_KEY] as User;
            if (SysBankBll.AlterStatus(bankid, status, user.SysOperator.ID, type) > 0)
            {
                result.flag = true;
                return Json(result);
            }
            result.flag = false;
            result.message = "注销失败";
            return Json(result);
        }

        [Privilege(Menu = "银行管理", Name = "编辑")]
        public ActionResult Edit()
        {
            var bankid = Convert.ToInt64(Request["id"]);
            var bank = SysBankBll.GetBankbyID(bankid);
            if (bank != null)
            {
                var model = new BankAddEdit();
                model.ID = bank.ID;
                model.Name = bank.Name;
                model.Number = bank.Number;
                model.Url = bank.Url;
                model.Type = bank.Type;
                model.ShortName = bank.ShortName;
                model.NiceName = bank.NiceName;
                model.Remark = bank.Remark;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Bank");
            }
        }

        [HttpPost]
        public ActionResult Edit(BankAddEdit model)
        {
            if (ModelState.IsValid)
            {
                var bank = SysBankBll.GetBankbyID(model.ID);
                model.Creator = ((User)Session[Constants.USER_KEY]).SysOperator.ID;
                if (bank == null)
                {
                    RedirectToAction("Index", "Bank");
                }
                //if (bank.Name.Trim() != model.Name.Trim())
                //{
                //    if (SysBankBll.GetBankbyName(model.Name.Trim()) != null)
                //    {
                //        ModelState.AddModelError("Name", "银行名称已存在");
                //        return View(model);
                //    }
                //}

                //上传图片
                HttpPostedFileBase file = Request.Files["Url"];
                if (file != null)
                {
                    if (file.FileName != "")
                    {
                        try
                        {
                            var filename =
                                Path.Combine(Request.MapPath("~/Upload"), file.FileName);
                            file.SaveAs(filename);

                            model.Url = "/Upload/" + file.FileName;
                        }
                        catch (Exception ex)
                        {
                            return Content(string.Format("上传文件出现异常：{0}", ex.Message));
                        }
                    }
                    if (!SysBankBll.EditBank(model))
                    {
                        TempData["returnMsgType"] = "error";
                        TempData["returnMsg"] = "修改失败";
                        return View(model);
                    }
                    TempData["resultMsgType"] = "success";
                    TempData["resultMsg"] = "编辑成功";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Url", "请选择图标");
                    return View(model);
                }

            }
            return View(model);
        }

        public ActionResult CheckName()
        {
            var bankname = Request["name"];
            var result = new AjaxResult();
            if (SysBankBll.GetBankbyName(bankname) != null)
            {
                result.flag = false;
                result.message = "银行名称已存在";
                return Json(result);
            }
            result.flag = true;
            result.message = "";
            return Json(result);
        }
    }
}