using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Controllers;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    public class SysLoginLogController : SysPrivilegeController
    {
        // GET: System/SysLoginLog
        ISysLoginLogBll sysLoginLogBll;
        public SysLoginLogController(ISysLoginLogBll sysLoginLogBll)
        {
            this.sysLoginLogBll = sysLoginLogBll;
        }

        [MenuItem(Parent = "账号信息", Name = "登录日志", Icon = "glyphicon glyphicon-bell")]
        [Privilege(Menu = "登录日志", Name = "查看")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string actionName)
        {
            switch (actionName.ToLower())
            {
                case "datagrid":
                    return Logs();
                default:
                    return Content("");
            }
        }

        private ActionResult Logs()
        {
            string currentPage = Request["page"];
            string pagesize = Request["rows"];
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

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

            var operater = ((User)HttpContext.Session[Common.Constants.USER_KEY]).SysOperator.ID;
            var entitys = sysLoginLogBll.getSysLoginLogGrid(operater, startTime, endTime);

            List<SysLoginLog> list = entitys.Skip((page - 1) * size).Take(size).ToList();
            int count = entitys.Count();

            DataGrid<SysLoginLog> result = new DataGrid<SysLoginLog>();
            result.rows = list;
            result.total = count;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}