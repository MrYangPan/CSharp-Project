using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Richnova.CEMS.Framework.Web.Authorization;
using Richnova.CEMS.Framework.Web.Filter;

namespace Richnova.CEMS.Application.Enterprise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Change ControllerSuffix from default string "Controller" to "ApiController"
            var suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null) suffix.SetValue(null, "ApiController");

            config.Filters.Add(new WebApiAuthorizeAttribute());
            config.Filters.Add(new WebApiExceptionFilter());
            config.Filters.Add(new WebApiDisposeFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 取消注释下面的代码行可对具有 IQueryable 或 IQueryable<T> 返回类型的操作启用查询支持。
            // 若要避免处理意外查询或恶意查询，请使用 QueryableAttribute 上的验证设置来验证传入查询。
            //config.EnableQuerySupport();

            // 若要在应用程序中禁用跟踪，请注释掉或删除以下代码行
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
