using System.Web.Mvc;
using Richnova.CEMS.Framework.Web.Authentication;

namespace Richnova.CEMS.Application.Enterprise.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            LoginHelper.SingOut();
            return Redirect("~/Login");
        }

        public ActionResult TimeOut()
        {
            LoginHelper.SingOut();
            return Redirect("~/Login");
        }
    }
}