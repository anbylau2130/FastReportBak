using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Globalization;
using USP.Utility;
using USP.Common;
namespace USP.Filters
{
    public class BenchmarkFilte : ActionFilterAttribute
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("BenchmarkLogger");
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            filterContext.HttpContext.Items[Constants.ACTION_DURATION] = stopWatch;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Items.Contains(Constants.ACTION_DURATION))
            {
                return;
            }
            var stopWatch = filterContext.HttpContext.Items[Constants.ACTION_DURATION] as Stopwatch;
            if (stopWatch == null)
            {
                return;
            }
            stopWatch.Stop();
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var url = filterContext.HttpContext.Request.Url;

            logger.Debug(String.Format("{0},Execute {1}-{2}:{3}ms]", url, controllerName, actionName, stopWatch.ElapsedMilliseconds));
            var response = filterContext.HttpContext.Response;
            if (response != null)
            {
                response.AddHeader("TotalSpendTimes",
                    stopWatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}