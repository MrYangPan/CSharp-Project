using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Service.Auth
{
    public class MenuService
    {
        protected NHibernateRepository Repository { get; set; }

        public IList<Menu> GetMenuById(string id)
        {
            var guid = new Guid(id);
            var tt = Repository.GetQueryable<Menu>();
            return Repository.GetQueryable<Menu>().Where(t => t.Id == guid).ToList();
        }

        public IList<Menu> AllMenus()
        {
            var lang = Thread.CurrentThread.CurrentCulture.Name;
            return Repository.GetList<Menu>(); 
        }

        public IList<Menu> Menus()
        {
            var lang = Thread.CurrentThread.CurrentCulture.Name;
            return Repository.GetQueryable<Menu>()
                .Where(t =>t.ParentId == null && t.Lang == lang)
                .OrderBy(t => t.OrderIndex)
                .ToList();
        }

        public IList<Menu> MyMenus(Guid userId)
        {
            var repositoriedUser = Repository.Load<User>(userId);
            var roleRsrc = (from role in repositoriedUser.Roles select role.Resources).ToList();

            return null;
        }

        public void Save(Menu menu)
        {
            if (!menu.Id.HasValue && menu.OrderIndex == 0)
                menu.OrderIndex =Repository.GetQueryable<Menu>().Where(o => o.ParentId == menu.ParentId).Max(o => o.OrderIndex);
            Repository.SaveOrUpdate(menu);
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<Menu>(id);
        }
    }
}
