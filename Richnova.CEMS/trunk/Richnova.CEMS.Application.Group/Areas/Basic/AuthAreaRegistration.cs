using System.Web.Http;
using System.Web.Mvc;

namespace Richnova.CEMS.Application.Group.Areas.Basic
{
    public class BasicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Basic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                AreaName + "_default",
                AreaName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Richnova.CEMS.Application.Group.Areas." + AreaName + ".Controllers" }
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                AreaName + "Api",
                "api/" + this.AreaName + "/{controller}/{action}/{id}",
                new
                {
                    area = this.AreaName,
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional,
                    namespaceName = new[] { "Richnova.CEMS.Application.Group.Areas." + AreaName + ".Controllers" }
                }
             );
        }
    }
}