using System.Web.Mvc;

namespace Richnova.CEMS.Application.Group.Controllers
{
    public class LocalController : Controller
    {
        public ActionResult en()
        {
            var en = new System.Globalization.CultureInfo("en-US");
            Session["Culture"] = en;
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult cn()
        {
            var cn = new System.Globalization.CultureInfo("zh-CN");
            Session["Culture"] = cn;
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult tw()
        {
            var tw = new System.Globalization.CultureInfo("zh-TW");
            Session["Culture"] = tw;
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
