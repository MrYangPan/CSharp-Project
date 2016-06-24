using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Richnova.CEMS.Entity.Basic;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Service.Basic
{
    public class BCAreaManagementService
    {
        protected NHibernateRepository Repository { get; set; }
        public IList<BCAreaManagement> GetAreas()
        {
            return Repository.GetQueryable<BCAreaManagement>()
                .OrderBy(t => t.AreaLevel)
                .ToList();
        }
        public List<BCAreaManagement> GetChildAreas(Guid? Id)
        {
            return Repository.GetList<BCAreaManagement>()
                   .OrderBy(t => t.AreaLevel)
                   .Where(p=>p.ParentId==Id).ToList();
        }
        public BCAreaManagement GetAreaById(Guid? Id)
        {
            return Repository.GetList<BCAreaManagement>()
                   .OrderBy(t => t.AreaLevel)
                   .Where(p => p.Id == Id).ToList().FirstOrDefault();
        }
        public BCAreaManagement GetParentArea(Guid? ParentId)
        {
            return Repository.GetList<BCAreaManagement>().Where(p => p.Id == ParentId).FirstOrDefault();
        }

        public void Save(BCAreaManagement bcAreaManagement)
        {
            Repository.SaveOrUpdate(bcAreaManagement);
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<BCAreaManagement>(id);
        }
    }
     public class NewArea
     {
         public string Id { get; set; }
         public string AreaName { get; set; }
         public string ParentAreaName { get; set; }
         public string Desc { get; set; }
         public int Level { get; set; }
     }
}
