using System;
using System.Collections.Generic;
using System.Linq;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Data;
using Richnova.CEMS.Framework.Utility.Encrypt;
using Richnova.CEMS.Framework.Web.Result;

namespace Richnova.CEMS.Service.Auth
{
    public class UserService
    {
        protected NHibernateRepository Repository { get; set; }

        public IList<User> Users()
        {
            return Repository.GetQueryable<User>()
                .OrderBy(t => t.UserId)
                .ToList();
        }

        public void Save(User user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                if (user.Id.HasValue){
                    user.Password = Repository.Load<User>(user.Id).Password;
                    Repository.Clear();
                }
            }
            else
            {
                user.Password = EncryptHelper.MD5(user.Password);
                Repository.SaveOrUpdate(user);
            }
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<User>(id);
        }

        public ActionResult<User> ChangePwd(string userId, string oldPwd, string newPwd)
        {
            var user = Repository.GetQueryable<User>().FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return new ActionResult<User> { Success = false, Message = "用户不存在！" };
            if (user.Password != EncryptHelper.MD5(oldPwd))
                return new ActionResult<User> { Success = false, Message = "原始密码错误！" };
            user.Password = EncryptHelper.MD5(newPwd);
            Repository.Save(user);
            return new ActionResult<User> { Success = true, Message = "密码修改成功！" };
        }
    }
}
