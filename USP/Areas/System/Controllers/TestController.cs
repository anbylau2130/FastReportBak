using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Attributes;
using USP.Bll;
using USP.Models.POCO;

namespace USP.Areas.System.Controllers
{
    [Menu(Name = "Common", Icon = "panel-icon glyphicon glyphicon-list")]
    public class TestController : Controller
    {
        ISysAreaBll sysAreaBll;
        public TestController(ISysAreaBll sysAreaBll)
        {
            this.sysAreaBll = sysAreaBll;
        }

        // GET: System/Test
        [MenuItem(Parent = "Common", Name = "图表列表", Icon = "glyphicon glyphicon-bell")]
        public ActionResult Index()
        {
            return View();
        }
        [MenuItem(Parent = "Common", Name = "地区联动", Icon = "glyphicon glyphicon-bell")]
        public ActionResult Area()
        {
            ViewBag.Province = "130000";
            ViewBag.Area = "130200";
            ViewBag.County = "130203";
            ViewBag.Community = "130203002";
            return View();
        }

        public ActionResult Curve()
        {
            return View();
        }

        public ActionResult CurveData()
        {
            var curve = new Curves()
            {
                //Month = "'一月','二月', '三月', '四月', '五月', '六月','七月', '八月',' 九月', '十月', '十一月', '十二月'",
                Month = "\"一月\",\"二月\", \"三月\", \"四月\", \"五月\", \"六月\",\"七月\", \"八月\", \"九月\", \"十月\", \"十一月\", \"十二月\"",
                RainFall = "49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4",
                Temperature = "7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6"
            };
            return Json(curve, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Pie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PieData()
        {
            List<Browers> lists = new List<Browers>();
            lists.Add(new Browers() { name = "浏览器1", y = 45 });
            lists.Add(new Browers() { name = "浏览器2", y = 26.8 });
            lists.Add(new Browers() { name = "浏览器3", y = 12.8 });
            lists.Add(new Browers() { name = "浏览器4", y = 8.5 });
            lists.Add(new Browers() { name = "浏览器5", y = 6.2 });
            lists.Add(new Browers() { name = "浏览器6", y = 0.7 });
            return Json(lists, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 86400)]
        public ActionResult GetProvinceData(string selectedId)
        {
            var list = new List<SelectOption>();

            list.Add(new SelectOption() { id = "", text = "==请选择==", selected = string.IsNullOrEmpty(selectedId) });

            list.AddRange(sysAreaBll.GetProvinces(selectedId));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration = 86400)]
        public ActionResult GetChildrens(string parentId, string selectedId)
        {
            var list = new List<SelectOption>();

            list.Add(new SelectOption() { id = "", text = "==请选择==", selected = string.IsNullOrEmpty(selectedId) });

            list.AddRange(sysAreaBll.GetAreasByParent(parentId, selectedId));

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }


    public class Curves
    {
        public string Month;
        public string RainFall;
        public string Temperature;
    }

    public class Browers
    {
        public string name;
        public double y;
    }

}