using System;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Auth
{
    [Class(Table = "UserSettings", Schema = "Auth", NameType = typeof(UserSettings))]
    public class UserSettings : NHibernateEntity
    {
        [Property(Column = "Theme", Length = 30, NotNull = true)]
        public virtual string Theme { get; set; }

        [Property(Column = "Navigation", Length = 30, NotNull = true)]
        public virtual string Navigation { get; set; }

        [Property(Column = "GridRows", NotNull = true)]
        public virtual int GridRows { get; set; }

        [Property(Column = "MaxTabCount", NotNull = true)]
        public virtual int MaxTabCount { get; set; }

        [Property(Column = "UserId", NotNull = true)]
        public virtual Guid? UserId { get; set; }
    }
}
