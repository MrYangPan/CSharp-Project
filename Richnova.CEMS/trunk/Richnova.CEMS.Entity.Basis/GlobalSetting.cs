using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Basis
{
    [Class(Table = "GlobalSetting", Schema = "Basis", NameType = typeof(GlobalSetting))]
    public class GlobalSetting : NHibernateEntity
    {
        [Property(Column = "ProductName", Length = 64, NotNull = true)]
        public string ProductName { get; set; }

        [Property(Column = "ProductEngName", Length = 64, NotNull = true)]
        public string ProductEngName { get; set; }
    }
}
