using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace USP.Filters
{
    public class LoggerFilte : FilterAttribute, IActionFilter, IResultFilter, IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

        public void OnException(ExceptionContext filterContext)
        {

        }
    }
}