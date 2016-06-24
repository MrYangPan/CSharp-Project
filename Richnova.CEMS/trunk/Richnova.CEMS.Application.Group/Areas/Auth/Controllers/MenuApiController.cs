using System;
using System.Threading;
using System.Web.Http;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Framework.Web.EasyUI;
using Richnova.CEMS.Service.Auth;

namespace Richnova.CEMS.Application.Group.Areas.Auth.Controllers
{
    public class MenuApiController : ApiController
    {
        protected MenuService MenuService { get; set; }

        [HttpPost]
        public dynamic GetMenus()
        {
            return MenuService.Menus();
        }

        [HttpPost]
        public dynamic GetMyMenus()
        {
            var userId = new Guid(LoginHelper.GetCurrentUser().User);
            return MenuService.MyMenus(userId);
        }

        [HttpPost]
        public dynamic Query()
        {
            var menus = MenuService.Menus();
            return new EasyUiGridData
            {
                rows = menus,
                total = menus.Count
            };
        }

        [HttpPost]
        public dynamic Delete(string id)
        {
            try
            {
                MenuService.Delete(new Guid(id));
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }

        [HttpPost]
        public dynamic Save(Menu menu)
        {
            try
            {
                menu.Lang = Thread.CurrentThread.CurrentCulture.Name;
                if (menu.Id.HasValue)
                    menu.UpdatedBy = LoginHelper.GetCurrentUser().Name;
                else
                    menu.CreatedBy = LoginHelper.GetCurrentUser().Name;
                MenuService.Save(menu);
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }
    }
}
