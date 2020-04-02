using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UTMusic.Web.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/css/all.min.css",
                "~/Content/mdbootstrap/css/bootstrap.min.css",
                "~/Content/mdbootstrap/css/mdb.min.css",
                "~/Content/mdbootstrap/css/style.css",
                "~/Content/plyr.css",
                "~/Content/main.css"));
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Content/mdbootstrap/js/popper.min.js",
                "~/Content/mdbootstrap/js/bootstrap.min.js",
                "~/Content/mdbootstrap/js/mdb.min.js",
                "~/Scripts/html5media.min.js",
                "~/Scripts/plyr.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/myPlayer/js").Include("~/Scripts/myPlayer.js"));
            bundles.Add(new ScriptBundle("~/bundles/myPlayerInit/js").Include("~/Scripts/myPlayerInit.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include("~/Scripts/jquery-3.4.1.min.js"));
        }
    }
}