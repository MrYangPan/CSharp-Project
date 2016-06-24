using System;
using System.Linq;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Data;
using Richnova.CEMS.Framework.Utility.Encrypt;
using Richnova.CEMS.Framework.Web.Result;

namespace Richnova.CEMS.Service.Auth
{

    public class LoginService 
    {
        protected NHibernateRepository Repository { get; set; }

        public ActionResult<User> Login(string uid, string pwd)
        {
            //用户名密码检查
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pwd))
                return new ActionResult<User> {Success = false, Message ="用户名和密码为空"};

            //用户名密码验证
            var checkUser = Repository.GetQueryable<User>().FirstOrDefault(u => u.UserId == uid && u.Password == EncryptHelper.MD5(pwd));
            if (checkUser == null)
                return new ActionResult<User> { Success = false, Message = "用户名和密码错误" };

            if(checkUser.IsLocked)
                return new ActionResult<User> { Success = false, Message = "用户已锁定，请联系管理员" };

            //返回登陆成功
            return new ActionResult<User>
            {
                Success = true,
                Message ="登录成功",
                Data = checkUser
            };
        }

        public UserSettings GetUserSettings(Guid? userId)
        {
            return Repository.GetQueryable<UserSettings>().FirstOrDefault(s => s.UserId == userId);
        }


    }
}