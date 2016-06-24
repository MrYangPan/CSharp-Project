using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Richnova.CEMS.Application.Group.Areas.Basic.Controllers
{
    public class BCIndustryManagementController : Controller
    {
        // GET: Basic/BCIndustryManagement
        public ActionResult Index()
        {
            var model = new
            {
                urls = new 
                {
                    query = "/api/Basic/BCAreaManagement/Query",
                    delete = "/api/Basic/BCAreaManagement/Delete",
                    save = "/api/Basic/BCAreaManagement/Save"
                }
            };return View(model);
        }

        public ActionResult Eidet()
        {
            var model = new
            {
                urls = new 
                {
                    save = "/api/Basic/BCAreaManagement/Save"
                }
            }; return View(model);
        }
    }
}