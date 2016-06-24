using System.Web.Mvc;

namespace Richnova.CEMS.Framework.Web.Filter
{
    public class MvcExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}