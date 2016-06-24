using Richnova.CEMS.Entity.Basic;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Framework.Web.EasyUI;
using Richnova.CEMS.Service.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Richnova.CEMS.Application.Group.Areas.Basic.Controllers
{
    public class BCIndustryManagementApiController : ApiController
    {
        protected BCIndustryManagementService BCIndustryManagement { get; set; }

        [HttpPost]
        public dynamic Query()
        {
            var areas = BCIndustryManagement.GetIndustry();
            return new EasyUiGridData
            {
                rows = areas,
                total = areas.Count
            };
        }

        public dynamic Delete(string id)
        {
            try
            {
                BCIndustryManagement.Delete(new Guid(id));
                return new { succes = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }

        public dynamic Save(BCIndustryManagement bcIndustryManagement)
        {
            try
            {
                if (bcIndustryManagement.Id.HasValue)
                    bcIndustryManagement.UpdatedBy = LoginHelper.GetCurrentUser().Name;
                else
                    bcIndustryManagement.CreatedBy = LoginHelper.GetCurrentUser().Name;
                BCIndustryManagement.Save(bcIndustryManagement);
                return new {succes=true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }
    }
}
