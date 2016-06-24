using System;
using System.Web.Http;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Framework.Web.EasyUI;
using Richnova.CEMS.Service.Auth;

namespace Richnova.CEMS.Application.Group.Areas.Auth.Controllers
{
    public class RoleApiController : ApiController
    {
        protected RoleService RoleService { get; set; }

        [HttpPost]
        public dynamic Query()
        {
            var roles = RoleService.Roles();
            return new EasyUiGridData
            {
                rows = roles,
                total = roles.Count
            };
        }

        [HttpPost]
        public dynamic Delete(string id)
        {
            try
            {
                RoleService.Delete(new Guid(id));
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }

        [HttpPost]
        public dynamic Save(Role role)
        {
            try
            {
                if (role.Id.HasValue)
                    role.UpdatedBy = LoginHelper.GetCurrentUser().Name;
                else
                    role.CreatedBy = LoginHelper.GetCurrentUser().Name;
                RoleService.Save(role);
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }
    }
}
