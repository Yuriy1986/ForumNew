using System.Web;
using System.Web.Optimization;

namespace ForumNew.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // For textarea.
            bundles.Add(new ScriptBundle("~/bundles/autosize").Include(
            "~/Scripts/autosize.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
            //"~/Scripts/jquery.unobtrusive-ajax.min.js"
            //));

            // Dialog.
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui.js"
            ));
            bundles.Add(new StyleBundle("~/Content/jquicss").Include(
          "~/Content/jquery-ui.css",
          "~/Content/jquery-ui.theme.css"));

            //// PagedList.
            //bundles.Add(new StyleBundle("~/Content/PagedList").Include(
            //"~/Content/PagedList.css"));
        }
    }
}
