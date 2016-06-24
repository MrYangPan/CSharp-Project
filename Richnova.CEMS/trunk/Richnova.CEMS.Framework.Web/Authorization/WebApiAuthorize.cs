using System.Web.Http;
using System.Web.Http.Controllers;
using Richnova.CEMS.Framework.Web.Authentication;

namespace Richnova.CEMS.Framework.Web.Authorization
{
    public class WebApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext))
                return false;

            if (!LoginHelper.ValidateToken())
                return false;

            return true;
        }
    }     
}