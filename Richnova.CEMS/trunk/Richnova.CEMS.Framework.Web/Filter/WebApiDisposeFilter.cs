using System;
using System.Web.Http.Filters;

namespace Richnova.CEMS.Framework.Web.Filter
{
    public class WebApiDisposeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            GC.Collect();
        }
    }
}