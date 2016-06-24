using System.Web.Mvc;

namespace Richnova.CEMS.Application.Group.Areas.Auth.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Org()
        {
            var model = new
            {
                urls = new
                {
                    query = "/api/Auth/Org/Query",
                    delete = "/api/Auth/Org/Delete",
                    save = "/api/Auth/Org/Save"
                }
            };
            return View(model);
        }

        public ActionResult User()
        {
            var model = new
            {
                urls = new
                {
                    query = "/api/Auth/User/Query",
                    delete = "/api/Auth/User/Delete",
                    save = "/api/Auth/User/Save",
                    rolePickDlg = "/Auth/Auth/RolePickDlg",
                    roles = "/api/Auth/Role/Query"
                }
            }; return View(model);
        }

        public ActionResult Role()
        {
            var model = new
            {
                urls = new
                {
                    query = "/api/Auth/Role/Query",
                    delete = "/api/Auth/Role/Delete",
                    save = "/api/Auth/Role/Save"
                }
            };
            return View(model);
        }

        public ActionResult RolePickDlg()
        {
            return View();
        }

        public ActionResult Authorization()
        {
            return View();
        }

        public ActionResult DataRule()
        {
            return View();
        }

        public ActionResult Resource()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var model = new
            {
                urls = new
                {
                    query = "/api/Auth/Menu/Query",
                    delete = "/api/Auth/Menu/Delete",
                    edit = "/Auth/Auth/MenuEdit"
                },
                msg = new
                {
                    confirmDeleteMenu = "删除菜单，有可能同时删除其子菜单，是否继续 [Yes] or [No]?",
                    confirmDeleteUser = "删除用户，将测底清除其授权，是否继续 [Yes] or [No]?"
                }
            };
            return View(model);
        }

        public ActionResult MenuEdit()
        {
            var model = new
            {
                urls = new
                {
                    save = "/api/Auth/Menu/Save"
                }
            };
            return View(model);
        }

        public ActionResult Button()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }
    }
}
