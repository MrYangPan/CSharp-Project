using System.Web.Http;
using Newtonsoft.Json.Linq;
using Richnova.CEMS.Framework.Web.Authentication;
using Richnova.CEMS.Service.Auth;

namespace Richnova.CEMS.Application.Enterprise.Controllers
{
    [AllowAnonymous]
    public class LoginApiController : ApiController
    {
        protected LoginService LoginService { get; set; }

        public dynamic Post([FromBody]JObject request)
        {
            var userCode = request.Value<string>("usercode");
            var password = request.Value<string>("password");
            var result = LoginService.Login(userCode, password);

            if (result.Success)
            {
                //调用Web应用登陆机制
                var currentUser = new CurrentUser
                {
                    User = result.Data.UserId,
                    Name = result.Data.FullName
                };
                const int effectiveHours = 8; //从系统参数获取登录时间
                LoginHelper.SignIn(currentUser, 60 * effectiveHours);

                //读用户参数
                var userSettings = LoginService.GetUserSettings(result.Data.Id);
                LoginHelper.SetSettiings(new Settings { Theme = userSettings.Theme, GridRows = userSettings.GridRows, MaxTabCount = userSettings.MaxTabCount, Navigation = userSettings.Navigation});

                //TODO 更新用户登陆次数及时间
                //TODO 添加登陆日志
            }
            return result;
        }
    }
}
