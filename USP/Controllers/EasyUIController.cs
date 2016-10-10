using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Bll;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using USP.Attributes;

namespace Test.Controllers
{
    //[Menu(Name = "Easui", Icon = "glyphicon glyphicon-eye-open")]
    public class EasyUIController : Controller
    {
        ISysAreaBll areaBll;
        public EasyUIController(ISysAreaBll areaBll)
        {
            this.areaBll = areaBll;
        }

        //[MenuItem(Parent = "Easui", Name = "Sample", Icon = "glyphicon glyphicon-bell")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Json()
        {
            AjaxResult result = new AjaxResult();
            result.message = "information信息";
            result.dateTime = DateTime.Now;
            result.attachment = new TreeNode();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaTree()
        {
            /*
            List<TreeNode> result = new List<TreeNode>();
            TreeNode tree = new TreeNode();
            tree.id = 1;
            tree.text = "Node 1";
            tree.state = "closed";
            tree.children = new List<TreeNode>();
            result.Add(tree);
            TreeNode tree1 = new TreeNode();
            tree1.id = 11;
            tree1.text = "Node 11";
            tree.children.Add(tree1);
            TreeNode tree2 = new TreeNode();
            tree1.id = 11;
            tree1.text = "Node 11";
            tree.children.Add(tree1);
            return Json(result, JsonRequestBehavior.AllowGet);
            */
           return Json(areaBll.getAreaTree(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaGrid(int page, int rows, string order, string orderType)
        {
            DataGrid<UP_ShowArea_Result> result = new DataGrid<UP_ShowArea_Result>();
            result = areaBll.getAreaGrid(page, rows, order, orderType);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}