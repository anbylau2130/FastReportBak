using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USP.Common;

namespace USP.Filters
{
    public class AuthorizeFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session[Constants.USER_KEY] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}