using System.Web;
using System.Web.Mvc;
using Richnova.CEMS.Framework.Web.Authentication;

namespace Richnova.CEMS.Framework.Web.Authorization
{
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase context)
        {
            if (!base.AuthorizeCore(context))
                return false;

            if (!LoginHelper.ValidateToken())
                return false;

            return true;
        }
    }     
}