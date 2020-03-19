using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UTMusic.Web.App_Start
{
    public static class BundlesConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/mdbootstrap/css/bootstrap.min.css",
                "~/Content/mdbootstrap/css/mdb.min.css",
                "~/Content/mdbootstrap/css/style.css",
                "~/Content/plyr.css",
                "~/Content/main.css"));
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Content/mdbootstrap/js/jquery.min.js",
                "~/Content/mdbootstrap/js/popper.min.js",
                "~/Content/mdbootstrap/js/bootstrap.min.js",
                "~/Content/mdbootstrap/js/mdb.min.js",
                "~/Scripts/html5media.min.js",
                "~/Scripts/plyr.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/myPlayer/js").Include("~/Scripts/myPlayer.js"));
        }
    }
}