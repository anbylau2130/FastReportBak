using FastReport;
using FastReport.Utils;
using FastReport.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Controllers;
using System.Web.UI.WebControls;
using USP.Context;
using USP.Bll.K3CLoud;
using USP.Models.POCO;
using USP.Common;
using USP.Models.Entity;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System;
using USP.Utility;

namespace USP.Areas.FastReportTest.Controllers
{
    [USP.Attributes.Menu(Name = "报表模块", Icon = "icon-building")]
    public class FastReportController : SysPrivilegeController
    {
        ISysOperatorBll sysOperatorBll;
        IEmployeeBll employeeBll;
        IHRORGBll hrOrgBll;
        IBranchBll branchBll;
        public FastReportController(IHRORGBll hrOrgBll,ISysOperatorBll sysOperatorBll, IEmployeeBll employeeBll,IBranchBll branchBll)
        {
            this.sysOperatorBll = sysOperatorBll;
            this.employeeBll = employeeBll;
            this.hrOrgBll = hrOrgBll;
            this.branchBll = branchBll;
        }


        //[USP.Attributes.MenuItem(Parent = "报表模块", Name = "员工信息报表", Icon = "icon-sitemap")]
        [Privilege(Menu = "员工信息报表查询", Name = "员工信息")]
        public ActionResult UserInfo()
        {
            string gonghao = Request["GongHao"];
            string date = Request["Date"];
            string zhiJieShangJi= Request["ZhiJieShangJi"];
            string fuZeRen = Request["FuZeRen"];
            var user = (User)HttpContext.Session[Common.Constants.USER_KEY];
            WebReport webReport = new WebReport();
            var templateName = GetReportUserInfoTemplate(RouteData.Values["controller"].ToString()).TrimStart().TrimEnd();
            if(templateName==string.Empty)
            {
                return Content("未能找到对应的模板!");
            }
            SetReport(webReport, templateName);
            ViewBag.WebReport = webReport;
            //if (string.IsNullOrEmpty(gonghao)&& string.IsNullOrEmpty(date))
            //{
            //    webReport.Report.Parameters.FindByName("工号").Value = user.SysOperator.LoginName;
            //    webReport.Report.Parameters.FindByName("日期").Value = DateTime.Now;
            //}
            //else
            //{
            webReport.Report.Parameters.FindByName("工号").Value = gonghao.Trim();
            webReport.Report.Parameters.FindByName("制表人").Value = user.SysOperator==null ?"": user.SysOperator.RealName.Trim();
            webReport.Report.Parameters.FindByName("直接上级").Value = zhiJieShangJi.Trim();
            webReport.Report.Parameters.FindByName("负责人").Value = fuZeRen.Trim();
            webReport.Report.Parameters.FindByName("日期").Value = Convert.ToDateTime(date) ;
            //}
            return View();
        }
        [USP.Attributes.MenuItem(Parent = "报表模块", Name = "员工信息报表查询", Icon = "icon-sitemap")]
        public ActionResult UserInfoSearch()
        {
            return View();
        }
       
        [Privilege(Menu = "经营分析表查询", Name = "经营分析表")]
        //[USP.Attributes.MenuItem(Parent = "报表模块", Name = "经营分析表查询", Icon = "icon-sitemap")]
        public ActionResult Management()
        {
            var ac = Request["actionName"] ?? "";
            if (!string.IsNullOrEmpty(ac))
            {
                return OtherAction(ac);
            }
            string ORGID = Request["ORGID"];
            string year = Request["MONTH"];
            string month = Request["YEAR"];
            var user = (User)HttpContext.Session[Common.Constants.USER_KEY];
            WebReport webReport = new WebReport();
            if (!string.IsNullOrEmpty(ORGID)&&!string.IsNullOrEmpty(year)&&!string.IsNullOrEmpty(month))
            {
                var templateName = GetReportManagement(RouteData.Values["controller"].ToString(), ORGID);
                if(templateName=="")
                {
                    return Content("未能找到对应的模板!ORGID："+ ORGID);
                }
                SetReport(webReport, templateName);
                webReport.Report.Parameters.FindByName("年").Value = year;
                webReport.Report.Parameters.FindByName("月").Value = month;
                webReport.Report.Parameters.FindByName("组织ID").Value = ORGID;
            }
            else
            {
                var templateName = GetReportManagement(RouteData.Values["controller"].ToString(), user.SysOperator.Remark); //remark存放ORGID
                if (templateName == "")
                {
                    return Content("未能找到对应的模板!ORGID："+ user.SysOperator.Remark);
                }
                SetReport(webReport, templateName);
                webReport.Report.Parameters.FindByName("年").Value = DateTime.Now.Year;
                webReport.Report.Parameters.FindByName("月").Value = DateTime.Now.Month;
                webReport.Report.Parameters.FindByName("组织ID").Value = user.SysOperator.Remark;
            }
            ViewBag.WebReport = webReport;
            return View();
        }
        [USP.Attributes.MenuItem(Parent = "报表模块", Name = "经营分析表查询", Icon = "icon-sitemap")]
        public ActionResult ManagementSearch()
        {
            var ac = Request["actionName"] ?? "";
            if (!string.IsNullOrEmpty(ac))
            {
                return OtherAction(ac);
            }
            return View();
        }
        [USP.Attributes.MenuItem(Parent = "报表模块", Name = "绩效考核表查询", Icon = "icon-sitemap")]
        public ActionResult KPISearch()
        {
            return View();
        }
        //[USP.Attributes.MenuItem(Parent = "报表模块", Name = "绩效考核表", Icon = "icon-sitemap")]
        [Privilege(Menu = "绩效考核表查询", Name = "绩效考核表")]
        public ActionResult KPI()
        {
            string gonghao = Request["GongHao"];
            var user = (User)HttpContext.Session[Common.Constants.USER_KEY];
            WebReport webReport = new WebReport();
            var templateName = GetKPITemplate(RouteData.Values["controller"].ToString()).TrimStart().TrimEnd();
            if (templateName == string.Empty)
            {
                return Content("未能找到对应的模板!");
            }
            SetReport(webReport, templateName);
            ViewBag.WebReport = webReport;
            if (string.IsNullOrEmpty(gonghao))
            {
                webReport.Report.Parameters.FindByName("工号").Value = user.SysOperator.LoginName;
            }
            else
            {
                webReport.Report.Parameters.FindByName("工号").Value = gonghao;
            }
            return View();
        }


        [USP.Attributes.MenuItem(Parent = "报表模块", Name = "门店盈利分析报表", Icon = "icon-sitemap")]
        public ActionResult BranchProfitAnalysisSearch()
        {
            var ac = Request["actionName"] ?? "";
            if (!string.IsNullOrEmpty(ac))
            {
                return OtherAction(ac);
            }
            return View();
        }
        [Privilege(Menu = "门店盈利分析报表", Name = "门店盈利分析")]
        public ActionResult BranchProfitAnalysis()
        {
            //日期
            string begindate = Request["BeginDate"];
            string enddate = Request["EndDate"];
            //门店编码
            string branchNumber = Request["BranchNumber"];
            WebReport webReport = new WebReport();
            var templateName = GetBranchProfitTemplate(RouteData.Values["controller"].ToString()).TrimStart().TrimEnd();
            if (templateName == string.Empty)
            {
                return Content("未能找到对应的模板!");
            }
            SetReport(webReport, templateName);
            ViewBag.WebReport = webReport;
            
            if (!string.IsNullOrEmpty(begindate)&& !string.IsNullOrEmpty(enddate) && !string.IsNullOrEmpty(branchNumber))
            {
                webReport.Report.Parameters.FindByName("DATESTR").Value = begindate;
                webReport.Report.Parameters.FindByName("DATEEND").Value = enddate;
                webReport.Report.Parameters.FindByName("FNUMBER").Value = branchNumber;
            }
            return View();
        }

        private string GetBranchProfitTemplate(string controller)
        {
            XmlOp xmlhelper = new XmlOp("../../App_Data/fastReport.config");
            var result = xmlhelper.GetXmlNodeValue("/Config/" + controller + "/BranchProfit");
            return result;
        }

        private ActionResult OtherAction(string ac)
        {
            switch (ac)
            {
                case "orgTree":
                    return Json(GetORGGroupTreeNode());
                case "branchDropDownList":
                    return Json(InitBranchDropDownList());
                default:
                    return Content("");
            }
        }
        private void SetReport(WebReport webReport,string templateName)
        {
            try
            {
                string report_path = GetReportPath();
                webReport.Report.Load(report_path + templateName);
                webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
                webReport.ShowExports = true;
                webReport.ShowPrint = true;
                webReport.Width = Unit.Percentage(100); // 
                webReport.Height = Unit.Percentage(100);// ; 
                webReport.ToolbarIconsStyle = ToolbarIconsStyle.Red;
                webReport.ToolbarStyle = ToolbarStyle.Small;
                webReport.ShowExports = true;
                webReport.ShowToolbar = true;
                webReport.ShowZoomButton = true;
                webReport.ShowPrint = true;
                webReport.AutoHeight = true;
               
            }
            catch(Exception ex)
            {
                LogUtil.Exception("Error", ex);
                throw;
            }
            
        }
        private string GetReportPath()
        {
            string report_path = Config.ApplicationFolder;
            XmlOp xmlhelper = new XmlOp("../../App_Data/fastReport.config");
            report_path += xmlhelper.GetXmlNodeAttributeValue("/Config/Reports", "Path");
            return report_path;
        }
        private string GetReportUserInfoTemplate(string Controller)
        {
            XmlOp xmlhelper = new XmlOp("../../App_Data/fastReport.config");
            var result = xmlhelper.GetXmlNodeValue("/Config/" + Controller + "/UserInfo");
            return result;
        }
        private string GetKPITemplate(string Controller)
        {
            XmlOp xmlhelper = new XmlOp("../../App_Data/fastReport.config");
            var result = xmlhelper.GetXmlNodeValue("/Config/" + Controller + "/KPI");
            return result;
        }
        private string GetReportManagement(string Controller,string ORGID)
        {
            XmlOp xmlhelper = new XmlOp("../../App_Data/fastReport.config");
            string xpath = "/Config/" + Controller + "/Management/ORG[@ORGID='"+ ORGID + "']";
            var result = xmlhelper.GetXmlNodeValue(xpath);
            return result;
        }
        public List<Models.POCO.TreeNode> GetORGGroupTreeNode()
        {
            List<HXN_HR_ORG_Group> orgGroup = hrOrgBll.GetORGGroupAll();
            List<HXN_HR_ORG_Group_L> orgGroupL = hrOrgBll.GetORGGroupLanguageAll();
            List<HXN_HR_ORG> org = hrOrgBll.GetORGAll();
            List<HXN_HR_ORG_L> orgL = hrOrgBll.GetORGLanguageALL();

            return GetORGGroupTreeNode(orgGroup, orgGroupL, org, orgL);
        }
        public List<Models.POCO.TreeNode> GetORGGroupTreeNode(List<HXN_HR_ORG_Group> orgGroup,
            List<HXN_HR_ORG_Group_L> orgGroupL,
            List<HXN_HR_ORG> org, List<HXN_HR_ORG_L> orgL,  long pid = 0)
        {
            //先取组
            List<Models.POCO.TreeNode> result = (from i in orgGroup
                                                 join iL in orgGroupL on i.FID equals iL.FID
                                     where i.FPARENTID == pid
                                     select new Models.POCO.TreeNode() { id = i.FID, text = iL.FNAME , attributes = new { type = "ORGGroup" } }
                                    ).ToList();
            if (result.Count == 0)
            {
                return GetORGTreeNode(org, orgL, pid, 0);
            }
            foreach (var item in result)
            {
                item.children = new List<Models.POCO.TreeNode>();
                item.children.AddRange(GetORGGroupTreeNode(orgGroup, orgGroupL, org, orgL,  item.id));
            }
            return result;

        }
        public List<Models.POCO.TreeNode> GetORGTreeNode(List<HXN_HR_ORG> org, List<HXN_HR_ORG_L> orgL,long groupId,long parent)
        {
            var result = (
                    from i in org
                    join iL in orgL on i.FID equals iL.FID
                    where i.FGROUPID == groupId //&&i.FPARENTID==parent
                    select new Models.POCO.TreeNode() { id = i.FID, text = iL.FNAME, attributes = new { type = "ORG" } }
                    ).ToList();
            if (result.Count == 0)
            {
                return result;
            }
            //foreach (var item in result)
            //{
            //    item.children = new List<Models.POCO.TreeNode>();
                
            //    //item.children.AddRange(GetORGTreeNode( org, orgL, groupId, item.id));
            //}
            return result;

        }

        public List<SelectListItem> InitBranchDropDownList()
        {
            return branchBll.GetBranchSelect().Select(item => new SelectListItem() { Text = item.Name, Value = item.FNumber }).ToList();
        }

        

    }
}
#region bak
//var userinfo = employeeBll.GetUserInfo(user.SysOperator.LoginName, user.SysOperator.Remark);
//var result=USP.Common.CommonDataTableExtensions.ListToDataTable(userinfo).ToList<UserInfo>();
//foreach (var item in result)
//{
//    employeeBll.AddUserInfo(item);
//}
//var temp= employeeBll.GetUserInfo(user.SysOperator.LoginName); 
//webReport.Report.RegisterData(userinfo, "User");
#endregion 