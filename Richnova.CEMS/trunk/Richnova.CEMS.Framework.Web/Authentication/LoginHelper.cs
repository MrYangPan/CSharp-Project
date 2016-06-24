using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Richnova.CEMS.Framework.Utility.Encrypt;
using Richnova.CEMS.Framework.Web.Request;

namespace Richnova.CEMS.Framework.Web.Authentication
{
    public class LoginHelper
    {
        const string SessionKey = "__user_settings__";

       /// <summary>
       /// 验证客户端Token是否正确 
       /// </summary>
        public static bool ValidateToken()
        {
            var currentUser = GetCurrentUser();
            if (string.IsNullOrEmpty(currentUser.User)) return false;
            var clientToken = currentUser.AuthToken;
            var serverToken = GetToken(currentUser.User);
            return serverToken.Equals(clientToken);
        }

        /// <summary>
        /// 从Forms验证票据中获取用户信息，来源Cookie
        /// </summary>
        public static CurrentUser GetCurrentUser()
        {
            var context = HttpContext.Current;
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null) return null;
            //获取Forms验证票据
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (ticket == null) return null;
            //从票据获取用户数据
            var loginInfo = JsonConvert.DeserializeObject<CurrentUser>(ticket.UserData);
            return loginInfo;
        }

        /// <summary>
        /// 服务器端生成用户Token
        /// Token由Uid + Sid构成，Uid经过用域名进行MD5加密
        /// </summary>
        /// <param name="loginName">登录名</param>
        private static string GetToken(string loginName)
        {
            //生成用户验证token
            const string key = "www.CCup.com";
            var uid = EncryptHelper.AESEncrypt(loginName, key);
            var sid = EncryptHelper.DESEncrypt(GetUniqueId());
            var md5 = EncryptHelper.MD5(uid + sid);
            var token = md5.Substring(Math.Min(loginName.Length, 10), 16);
            return token;
        }

        /// <summary>
        /// 获取唯一的Token ID
        /// </summary>
        /// <returns></returns>
        private static string GetUniqueId()
        {
            var authTokenType = "useragent";
            var context = HttpContext.Current;
            var request = context.Request;

            if (authTokenType == "sessionid")
            {
                if (HttpContext.Current.Session == null) throw new Exception();
                //mvc在没有设置session之前，每次sessionID都会变化，以下两句解决这个问题
                SessionHelper.Add("__Activation__", 1);
                SessionHelper.Remove("__Activation__");
                return context.Session.SessionID;
            }

            if (authTokenType == "useragent")
                return request.UserAgent + request.UserHostAddress + request.UserHostName + request.Url.Port;

            if (authTokenType == "ipadress")
                return RequestHelper.ClientIP;

            return string.Empty;
        }

        public static void SignIn(CurrentUser currentUser, int expireMin)
        {
            var loginName = currentUser.User;
            currentUser.AuthToken = GetToken(loginName);
            var data = JsonConvert.SerializeObject(currentUser);

            //创建一个FormsAuthenticationTicket，它包含登录名以及额外的用户数据。
            var ticket = new FormsAuthenticationTicket(2, loginName, DateTime.Now, DateTime.Now.AddDays(1), true, data);

            //加密Ticket，变成一个加密的字符串。
            var cookieValue = FormsAuthentication.Encrypt(ticket);

            //根据加密结果创建登录Cookie
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };
            if (expireMin > 0)
                cookie.Expires = DateTime.Now.AddMinutes(expireMin);

            var context = HttpContext.Current;
            if (context == null)
                throw new InvalidOperationException();

            //写登录Cookie
            CookiesHelper.Remove(cookie.Name);
            CookiesHelper.Add(cookie);
        }

        public static void SingOut()
        {
            FormsAuthentication.SignOut();
            ClearSettings();
        }

        public static Settings GetSettiings()
        {
            var sessionEnabled = HttpContext.Current.Session != null;
            Settings settings;
            if (sessionEnabled)
            {
                settings = SessionHelper.Get(SessionKey) as Settings;
                if (settings != null) return settings;
                settings = new Settings();
                SessionHelper.Add(SessionKey, settings);
            }
            else
            {
                settings = new Settings();
            }
            return settings;
        }

        public static void SetSettiings(Settings settings)
        {
            var sessionEnabled = HttpContext.Current.Session != null;
            if (sessionEnabled)
            {
                SessionHelper.Add(SessionKey, settings);
            }
        }

        public static void ClearSettings()
        {
            var sessionEnabled = HttpContext.Current.Session != null;
            if (sessionEnabled)
            {
                SessionHelper.Remove(SessionKey);
            }
        }
    }
}