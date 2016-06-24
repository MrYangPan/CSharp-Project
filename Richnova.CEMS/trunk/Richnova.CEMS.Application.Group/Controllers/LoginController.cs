using System.Web.Mvc;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Service.Basis;

namespace Richnova.CEMS.Application.Group.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private GlobalSettingService GlobalSettingService { get; set; }

        public ActionResult Index()
        {
            var model = new
            {
                globalSettings = GlobalSettingService.GetSettings()
            };

            return View(model);
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