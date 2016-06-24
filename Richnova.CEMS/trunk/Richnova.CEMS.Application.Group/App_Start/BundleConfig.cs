using System.Web.Optimization;

namespace Richnova.CEMS.Application.Group
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            ResetIgnorePatterns(bundles.IgnoreList);

            #region JS文件

            //JQuery
            bundles.Add(new ScriptBundle("~/Statics/Scripts/jquery/js").Include(
                "~/Statics/Scripts/jquery/jquery-{version}.js"));

            //EasyUI
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/cn").Include(
                "~/Statics/Scripts/easyui/easyui-lang-zh_CN.js"));
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/tw").Include(
                "~/Statics/Scripts/easyui/easyui-lang-zh_TW.js"));
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/en").Include(
                "~/Statics/Scripts/easyui/easyui-lang-en_US.js"));

            //HighCharts
            bundles.Add(new ScriptBundle("~/Statics/Scripts/highcharts/js").Include(
                "~/Statics/Scripts/highcharts/highcharts-all.js"));

            //Bootstrap
            bundles.Add(new ScriptBundle("~/Statics/Scripts/bootstrap").Include(
                "~/Statics/Scripts/bootstrap/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Statics/Scripts/respond").Include(
                "~/Statics/Scripts/respond/respond.js"));

            //knockout
            bundles.Add(new ScriptBundle("~/Statics/Scripts/knockout/js").Include(
                "~/Statics/Scripts/knockout/knockout-{version}.js"));

            //JSON2
            bundles.Add(new ScriptBundle("~/Statics/Scripts/common/json2").Include(
                "~/Statics/Scripts/common/json2.js"));

            //utils
            bundles.Add(new ScriptBundle("~/Statics/Scripts/common/utils").Include(
                "~/Statics/Scripts/common/utils.js"));

            //Language backage
            bundles.Add(new ScriptBundle("~/Statics/Scripts/lang/cn").Include(
                "~/Statics/Scripts/lang/app-lang-zh_CN.js"));

            #endregion

            #region 样式表
            bundles.Add(new StyleBundle("~/Statics/Styles/themes/default/css").Include(
                "~/Statics/Styles/themes/default/easyui.css"));
            bundles.Add(new StyleBundle("~/Statics/Styles/themes/icon").Include(
                "~/Statics/Styles/themes/icon.css"));

            bundles.Add(new StyleBundle("~/Statics/Styles/site").Include(
                "~/Statics/Styles/site.css"));
            bundles.Add(new StyleBundle("~/Statics/Styles/login").Include(
                "~/Statics/Styles/login.css"));

            bundles.Add(new StyleBundle("~/Statics/Scripts/bootstrap/css").Include(
                "~/Statics/Scripts/bootstrap/bootstrap.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }

        public static void ResetIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Clear();
            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}
