using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Auth
{
    [Class(Table = "Users", Schema = "Auth", NameType = typeof(User))]
    public class User : NHibernateEntity
    {
        [Property(Column = "FullName", Length = 32, NotNull = true)]
        public virtual string FullName { get; set; }

        [JsonIgnore]
        [Property(Column = "Password", Length = 128)]
        public virtual string Password { get; set; }

        [Property(Column = "UserId", Length = 32, NotNull = true,Unique = true)]
        public virtual string UserId { get; set; }

        [Property(Column = "StaffNo", Length = 32)]
        public virtual string StaffNo { get; set; }
        
        [Property(Column = "Alias", Length = 32)]
        public virtual string Alias { get; set; }

        [Property(Column = "Email", Length = 100)]
        public virtual string Email { get; set; }

        [Property(Column = "Mobile", Length = 20)]
        public virtual string Mobile { get; set; }

        [Property(Column = "IsLocked")]
        public virtual bool IsLocked { get; set; }

        [Property(Column = "LastLogin")]
        public virtual DateTime? LastLogin { get; set; }

        [Bag(1, Name = "Roles", Table = "UserRole", Schema = "Auth", Cascade = "all-delete-orphan", Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Subselect)]
        [Key(2, Column = "UserId")]
        [ManyToMany(3, ClassType = typeof(Role), Column = "RoleId")]
        public virtual IList<Role> Roles { get; set; }

        [ManyToOne(Name = "Organization", Column = "OrganizationId", Insert = false, Update = false, ClassType = typeof(Organization))]
        public virtual Organization Organization { get; set; }

    }
}
