using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Controllers;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Areas.System.Controllers
{
    public class ChangePasswordController : SysPrivilegeController
    {
        // GET: Operator/changePassword

        ISysOperatorBll operatorBll;

        public ChangePasswordController(ISysOperatorBll operatorBll)
        {
            this.operatorBll = operatorBll;
        }

        [HttpGet]
        [MenuItem(Parent = "账号信息", Name = "修改密码", Icon = "icon-edit")]
        [Privilege(Menu = "修改密码", Name = "修改")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RePassword password)
        {
            var operator1 = (User)HttpContext.Session[Common.Constants.USER_KEY];
            var model = operatorBll.GetOperatorbyId(operator1.SysOperator.ID);
            if (model.Password != Util.GetPassword(model.LoginName, password.OldPassword.Trim()))
            {
                TempData["resultMsgType"] = "error";
                TempData["resultMsg"] = "旧密码不正确";
                return View();
            }
            else
            {
                var result = operatorBll.AlterPassword(operator1.SysOperator.ID, Util.GetPassword(model.LoginName, password.NewPassword.Trim()));
                if (result > 0)
                {
                    TempData["resultMsgType"] = "success";
                    TempData["resultMsg"] = "修改成功";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["resultMsgType"] = "error";
                    TempData["resultMsg"] = "修改失败";
                    return View();
                }
            }
        }
    }
}