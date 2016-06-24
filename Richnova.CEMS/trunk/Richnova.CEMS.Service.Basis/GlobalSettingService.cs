using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Richnova.CEMS.Entity.Basis;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Service.Basis
{
    public class GlobalSettingService
    {
        protected NHibernateRepository Repository { get; set; }

        public GlobalSetting GetSettings()
        {
            return Repository.GetQueryable<GlobalSetting>().FirstOrDefault();
        }

        public void Save(GlobalSetting globalSetting)
        {
            Repository.SaveOrUpdate(globalSetting);
        }

        public void Delete(Guid? id)
        {
            Repository.PhysicsDelete<GlobalSetting>(id);
        }
    }}
