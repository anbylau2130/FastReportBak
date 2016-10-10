using System;
using System.Web;
using System.Web.Optimization;
using System.Web.WebPages;

namespace USP
{
    public class UspBundleConfig
    {
        public static void RegisterBundles()
        {
            BundleCollection bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/Js/usp").Include(
                     "~/Static/Js/usp/usp.js"
                            ));


        }
    }
}