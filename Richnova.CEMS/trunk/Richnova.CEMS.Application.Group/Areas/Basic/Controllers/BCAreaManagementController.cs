using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Richnova.CEMS.Application.Group.Areas.Basic.Controllers
{
    public class BCAreaManagementController : Controller
    {
        public ActionResult Index()
        {
            var model = new
            {
                urls = new
                {
                    grid = "/api/Basic/BCAreaManagement/Grid",
                    query = "/api/Basic/BCAreaManagement/Query",
                    delete = "/api/Basic/BCAreaManagement/Delete",
                    save = "/api/Basic/BCAreaManagement/Save",
                    edit = "/Basic/BCAreaManagement/Edit",
                    treeData = "/api/Basic/BCAreaManagement/TreeData",
                },
                msg = new
                {
                    confirmDeleteArea = "删除该行政区域，有可能同时删除其子行政区域，是否继续 [Yes] or [No]?",
                }
            };
            return View(model);
        }
        public ActionResult Edit()
        {
            var model = new
            {
                urls = new
                {
                    edit = "/api/Basic/BCAreaManagement/Edit",
                }
            };
            return View(model);
        }
    }
}