using System;
using System.Web.Mvc;

namespace Richnova.CEMS.Framework.Web.Filter
{
    public class MvcDisposeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            GC.Collect();
        }
    }
}