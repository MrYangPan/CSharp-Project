using System.Web.Mvc;
using Richnova.CEMS.Framework.Web.Authorization;
using Richnova.CEMS.Framework.Web.Filter;

namespace Richnova.CEMS.Application.Enterprise
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());

            filters.Add(new MvcExceptionFilter());
            filters.Add(new MvcAuthorizeAttribute());
            filters.Add(new MvcDisposeFilter());
        }
    }
}
