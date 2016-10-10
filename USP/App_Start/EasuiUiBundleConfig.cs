using System;
using System.Web;
using System.Web.Optimization;
using System.Web.WebPages;

namespace USP
{
    public class EasuiUiBundleConfig
    {
        public static void RegisterBundles()
        {
            BundleCollection bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/Js/easyui").Include(
                     "~/Static/Js/jquery.easyui-{version}.js",
                     "~/Static/Js/locale/easyui-lang-zh_CN.js"
                            ));

            bundles.Add(new StyleBundle("~/Css/easyui").Include(
                            "~/Static/Css/themes/icon.css",
                            "~/Static/Css/themes/bootstrap/easyui.css"
                        ));

        }
    }
}