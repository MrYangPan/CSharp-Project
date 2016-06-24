using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Auth
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [Class(Table = "Organization", Schema = "Auth", NameType = typeof (Organization))]
    public class Organization : NHibernateEntity
    {
        #region Properties

        [Property(Column = "Code", Length = 30)]
        public virtual string Code { get; set; }

        [Property(Column = "Name", Length = 30)]
        public virtual string Name { get; set; }

        [Property(Column = "Comments", Length = 1000)]
        public virtual string Comments { get; set; }
        #endregion

        #region Foreign Properties

        [Property(Column = "ParentId")]
        public virtual Guid? ParentId { get; set; }

         [JsonIgnore]
        [ManyToOne(Name = "Parent", Column = "ParentId", Insert = false, Update = false, ClassType = typeof (Organization))]
        public virtual Organization Parent { get; set; }

        [JsonIgnore]
        [Bag(1, Name = "Users", Cascade = "all-delete-orphan", Inverse = true, Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Join)]
        [Key(2, Column = "OrganizationId")]
        [OneToMany(3, ClassType = typeof(User))]
        public virtual IList<User> Users { get; set; }

        [JsonProperty(PropertyName = "children")]
        [Bag(1, Name = "Children", OrderBy = "Code", Cascade = "all-delete-orphan", Inverse = true, Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Subselect)]
        [Key(2, Column = "ParentId")]
        [OneToMany(3, ClassType = typeof(Organization))]
        public virtual IList<Organization> Children { get; set; }

        #endregion

        #region    仅用于EasyUI构建树JSON序列化使用使用
        [JsonProperty(PropertyName = "id")]
        protected Guid? id { get { return Id; } }
        [JsonProperty(PropertyName = "text")]
        protected string text { get { return Name; } }
        #endregion
    }
}
