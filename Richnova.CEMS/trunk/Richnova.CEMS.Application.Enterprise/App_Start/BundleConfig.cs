using System.Web.Optimization;

namespace Richnova.CEMS.Application.Enterprise
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
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/js").Include(
                "~/Statics/Scripts/easyui/jquery.easyui*"));

            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/cn").Include(
                "~/Statics/Scripts/easyui/easyui-lang-zn_CN.js"));
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/tw").Include(
                "~/Statics/Scripts/easyui/easyui-lang-zn_TW.js"));
            bundles.Add(new ScriptBundle("~/Statics/Scripts/easyui/en").Include(
                "~/Statics/Scripts/easyui/easyui-lang-en_US.js"));

            //HighCharts
            bundles.Add(new ScriptBundle("~/Statics/Scripts/highcharts/js").Include(
                "~/Statics/Scripts/highcharts/highcharts-all.js"));

            //Bootstrap
            bundles.Add(new ScriptBundle("~/Statics/Scripts/bootstrap").Include(
                "~/Statics/Scripts/bootstrap/bootstrap.js", 
                "~/Statics/Scripts/respond/respond.js"));

            //knockout
            bundles.Add(new ScriptBundle("~/Statics/Scripts/knockout/js").Include(
                "~/Statics/Scripts/knockout/knockout-{version}.js"));

            //JSON2
            bundles.Add(new ScriptBundle("~/Statics/Scripts/common/json2").Include(
                "~/Statics/Scripts/common/json2.js"));

            //Language backage
            bundles.Add(new ScriptBundle("~/Statics/Scripts/lang/cn").Include(
                "~/Statics/Scripts/lang/app-lang-zh_CN.js"));

            #endregion

            #region 样式表
            bundles.Add(new StyleBundle("~/Statics/Styles/easyui/themes/default/css").Include(
                "~/Statics/Scripts/easyui/themes/default/easyui.css"));

            bundles.Add(new StyleBundle("~/Statics/Styles/easyui/themes/icon").Include(
                "~/Statics/Scripts/easyui/themes/icon.css"));
            bundles.Add(new StyleBundle("~/Statics/Styles/site").Include(
                "~/Statics/Styles/site.css"));

            bundles.Add(new StyleBundle("~/Statics/Styles/login").Include(
                "~/Statics/Styles/login.css"));

            bundles.Add(new StyleBundle("~/Statics/Styles/bootstrap/css").Include(
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
