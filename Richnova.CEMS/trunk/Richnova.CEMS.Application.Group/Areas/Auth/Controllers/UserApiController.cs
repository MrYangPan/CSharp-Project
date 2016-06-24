using System;
using System.Web.Http;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Framework.Web.EasyUI;
using Richnova.CEMS.Service.Auth;

namespace Richnova.CEMS.Application.Group.Areas.Auth.Controllers
{
    public class UserApiController : ApiController
    {
        protected UserService UserService { get; set; }

        [HttpPost]
        public dynamic Query()
        {
            var users = UserService.Users();
            return new EasyUiGridData
            {
                rows = users,
                total = users.Count
            };
        }

        [HttpPost]
        public dynamic Delete(string id)
        {
            try
            {
                UserService.Delete(new Guid(id));
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }

        [HttpPost]
        public dynamic Save(User user)
        {
            try
            {
                if (user.Id.HasValue)
                    user.UpdatedBy = LoginHelper.GetCurrentUser().Name;
                else
                    user.CreatedBy = LoginHelper.GetCurrentUser().Name;
                UserService.Save(user);
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }
    }
}
