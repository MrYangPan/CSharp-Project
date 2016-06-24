using System;
using System.Web.Http;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Framework.Web.EasyUI;
using Richnova.CEMS.Service.Auth;
using Richnova.CEMS.Service.Basic;
using Richnova.CEMS.Entity.Basic;
using System.Collections.Generic;

namespace Richnova.CEMS.Application.Group.Areas.Basic.Controllers
{
    public class BCAreaManagementApiController : ApiController
    {
        protected BCAreaManagementService BCAreaManagementService { get; set; }
        [HttpPost]
        public dynamic Grid()
        {
            var areas = BCAreaManagementService.GetAreas();
            List<NewArea> areaList = new List<NewArea>();
            foreach (var item in areas)
            {
                NewArea area = new NewArea();
                area.Id = item.Id.ToString();
                area.AreaName = item.AreaName;
                area.ParentAreaName = string.IsNullOrEmpty(item.ParentId.ToString()) ? "" : BCAreaManagementService.GetParentArea(item.ParentId).AreaName;
                area.Desc = item.AreaDesc;
                area.Level = item.AreaLevel;
                areaList.Add(area);
            }
            return new EasyUiGridData
            {
                rows = areaList,
                total = areaList.Count
            };
        }
        public dynamic Query()
        {
            var areas = BCAreaManagementService.GetAreas();
            return new EasyUiGridData
            {
                rows = areas,
                total = areas.Count
            };
        }
        [HttpPost]
        public dynamic Delete(string id)
        {
            try
            {
                BCAreaManagementService.Delete(new Guid(id));
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }

        [HttpPost]
        public dynamic Save(BCAreaManagement bcAreaManagement)
        {
            try
            {
                if (bcAreaManagement.Id.HasValue)
                {
                    bcAreaManagement.UpdatedBy = LoginHelper.GetCurrentUser().Name;
                    bcAreaManagement.AreaLevel = BCAreaManagementService.GetParentArea(bcAreaManagement.ParentId).AreaLevel + 1;
                }
                else
                {
                    bcAreaManagement.CreatedBy = LoginHelper.GetCurrentUser().Name;
                    bcAreaManagement.AreaLevel = BCAreaManagementService.GetParentArea(bcAreaManagement.ParentId).AreaLevel + 1;
                }
                BCAreaManagementService.Save(bcAreaManagement);
                return new { success = true };
            }
            catch (Exception)
            {
                return new { success = false };
            }
        }
        public dynamic TreeData()
        {
            List<ComboTreeModel> comboTreeModelList = new List<ComboTreeModel>();
            var areas = BCAreaManagementService.GetAreas();
            foreach (var item in areas)
            {
                ComboTreeModel comboTreeModel = new ComboTreeModel();
                comboTreeModel.id = item.Id;
                comboTreeModel.text = item.AreaName;
                var areaChildren = BCAreaManagementService.GetChildAreas(item.Id);
                foreach (var obj in areaChildren)
                {
                    comboTreeModel.children = obj;
                    comboTreeModelList.Add(comboTreeModel);
                }
            }
            return Json(new { comboTreeData = comboTreeModelList });
        }
        public dynamic Edit(string id)
        {
            BCAreaManagement model=new BCAreaManagement();
            if (!string.IsNullOrEmpty(id))
            {
                model = BCAreaManagementService.GetAreaById(new Guid(id));
            }
            return Json(new { model = model });
        }
        public class ComboTreeModel
        {
            public Guid? id { get; set; }
            public String text { get; set; }
            public BCAreaManagement children { get; set; }
        }
    }
}
