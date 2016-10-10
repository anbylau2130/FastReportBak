using System.Web.Mvc;

namespace USP.Areas.FastReportTest
{
    public class FastReportTestAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FastReportTest";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FastReportTest_default",
                "FastReportTest/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}