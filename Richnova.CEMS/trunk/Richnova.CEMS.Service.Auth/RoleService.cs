using System;
using System.Collections.Generic;
using System.Linq;
using Richnova.CEMS.Entity.Auth;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Service.Auth
{
    public class RoleService
    {
        protected NHibernateRepository Repository { get; set; }

        public IList<Role> Roles()
        {
            return Repository.GetQueryable<Role>()
                .OrderBy(t => t.Name)
                .ToList();
        }

        public void Save(Role role)
        {
            Repository.SaveOrUpdate(role);
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<Role>(id);
        }
    }
}
