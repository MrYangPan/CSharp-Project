using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Richnova.CEMS.Entity.Basic;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Service.Basic
{
    public class BCIndustryManagementService
    {
        protected NHibernateRepository Repository { get; set; }

        public IList<BCIndustryManagement> GetIndustry()
        {
            return Repository.GetList<BCIndustryManagement>();
        }

        public BCIndustryManagement GetParentBCIndustry(Guid? ParentId)
        {
            return Repository.GetQueryable<BCIndustryManagement>().Where(p=>p.Id==ParentId).FirstOrDefault();
        }

        public void Save(BCIndustryManagement bcIndustryManagement)
        {
            Repository.SaveOrUpdate(bcIndustryManagement);
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<BCIndustryManagement>(id);
        }

    }
}
