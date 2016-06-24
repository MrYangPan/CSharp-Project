using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Richnova.CEMS.Entity.Basic
{
    [Class(Table = "IndustryManagement", Schema = "Basic", NameType = typeof(BCIndustryManagement))]
    public class BCIndustryManagement : NHibernateEntity
    {
        [Property(Column = "IndustryName", Length = 200, NotNull = true)]
        public virtual string IndustryName { get; set; }

        [Property(Column = "ParentId", Length = 50, NotNull = false)]
        public virtual string ParentId { get; set; }

        [Property(Column = "IndustryLevel", NotNull = true)]
        public virtual int IndustryLevel { get; set; }

        [Property(Column = "IndustryDesc", Length = 500, NotNull = false)]
        public virtual string IndustryDesc { get; set; }
    }
}
