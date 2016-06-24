using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Basic
{
    [Class(Table = "AreaManagement", Schema = "Basic", NameType = typeof(BCAreaManagement))]
    public class BCAreaManagement : NHibernateEntity
    {
        [Property(Column = "AreaName", Length = 50, NotNull = true)]
        public virtual string AreaName { get; set; }

        [Property(Column = "ParentId", Length = 50)]
        public virtual Guid? ParentId { get; set; }

        [Property(Column = "AreaLevel", NotNull = true)]
        public virtual int AreaLevel { get; set; }

        [Property(Column = "AreaDesc", Length = 500)]
        public virtual string AreaDesc { get; set; }
    }
}
