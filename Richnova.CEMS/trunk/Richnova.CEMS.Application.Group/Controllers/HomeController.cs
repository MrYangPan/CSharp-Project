using System.Web.Mvc;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Service.Auth;
using Richnova.CEMS.Service.Basis;

namespace Richnova.CEMS.Application.Group.Controllers
{
    public class HomeController : Controller
    {
        protected LoginService LoginService { get; set; }
        private GlobalSettingService GlobalSettingService { get; set; }

        public ActionResult Index()
        {
            InitApplication();

            var currentUser = LoginHelper.GetCurrentUser();
            var userSettings = LoginHelper.GetSettiings();
            var model = new
            {
                userName = currentUser.Name,
                navigation = userSettings.Navigation,
                theme = userSettings.Theme,
                maxTabCount = userSettings.MaxTabCount,
                gridRows = userSettings.GridRows,
                globalSettings = GlobalSettingService.GetSettings(),
                msg = new
                {
                    info = Lang.Message.Diag_Information,
                    warnning = Lang.Message.Diag_Warnning,
                    error = Lang.Message.Diag_Error,
                    saveSuccess = Lang.Message.CRUD_SaveSuccess,
                    saveFail = Lang.Message.CRUD_SaveFailed,
                    deleteSuccess = Lang.Message.CRUD_DeleteSuccess,
                    deleteFail = Lang.Message.CRUD_DeleteFailed,
                    deleteConfirm = Lang.Message.CRUD_DeleteConfirm,
                    noSelected = Lang.Message.Grid_NoSeleted
                }
            };
            return View(model);
        }

        private void InitApplication()
        {
            //初始化系统全局参数和用户参数
            var userSettings = LoginService.GetUserSettings(LoginHelper.GetCurrentUser().Id);
            LoginHelper.SetSettiings(new Settings { Theme = userSettings.Theme, GridRows = userSettings.GridRows, MaxTabCount = userSettings.MaxTabCount, Navigation = userSettings.Navigation });

            //初始化客户端JS提示信息语言包

        }
    }
}