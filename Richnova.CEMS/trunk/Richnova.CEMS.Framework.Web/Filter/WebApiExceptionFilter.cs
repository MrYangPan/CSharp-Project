using System.Net.Http;
using System.Web.Http.Filters;

namespace Richnova.CEMS.Framework.Web.Filter
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var message = context.Exception.Message;
            if (context.Exception.InnerException != null) 
                message = context.Exception.InnerException.Message;

            context.Response = new HttpResponseMessage() { Content = new StringContent(message) };

            base.OnException(context);
        }
    }
}