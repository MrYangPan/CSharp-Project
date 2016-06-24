using System.Collections.Generic;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Auth
{
    [Class(Table = "Role", Schema = "Auth", NameType = typeof (Role))]
    public class Role : NHibernateEntity
    {
        [Property(Column = "Name", Length = 30, NotNull = true)]
        public virtual string Name { get; set; }

        [Property(Column = "Comments", Length = 100)]
        public virtual string Comments { get; set; }

        [JsonIgnore]
        [Bag(1, Name = "Users", Table = "UserRole", Schema = "Auth", Cascade = "all-delete-orphan", Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Subselect)]
        [Key(2, Column = "RoleId")]
        [ManyToMany(3, ClassType = typeof(User), Column = "UserId")]
        public virtual IList<User> Users { get; set; }

        [JsonIgnore]
        [Bag(1, Name = "Resources", Table = "RoleResource", Schema = "Auth", Cascade = "all-delete-orphan", Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Subselect)]
        [Key(2, Column = "RoleId")]
        [ManyToMany(3, ClassType = typeof(Resource), Column = "ResourceId")]
        public virtual IList<Resource> Resources { get; set; }
    }
}
