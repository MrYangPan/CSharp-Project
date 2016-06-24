using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;
using Richnova.CEMS.Framework.Data;

namespace Richnova.CEMS.Entity.Auth
{
    [Class(Table = "Resource", Schema = "Auth", NameType = typeof(Resource))]
    public class Resource : NHibernateEntity
    {
        [Discriminator(Column = "ResourceType",Length = 10)]
        public virtual string ResourceType { get; set; }

        [Property(Column = "Lang", Length = 10, NotNull = true)]
        public virtual string Lang { get; set; }

        [Property(Column = "Name", Length = 30, NotNull = true)]
        public virtual string Name { get; set; }

        [Property(Column = "OrderIndex")]
        public virtual int OrderIndex { get; set; }

        [Property(Column = "IsHidden")]
        public virtual bool IsHidden { get; set; }

        [Property(Column = "Comments", Length = 255)]
        public virtual string Comments { get; set; }
    }

    /// <summary>
    /// 按钮
    /// </summary>
    [Subclass(NameType = typeof(Button), ExtendsType = typeof(Resource), DiscriminatorValue = "Button")]
    public class Button : Resource
    {

    }

    /// <summary>
    /// 菜单
    /// </summary>
    [Subclass(NameType = typeof(Menu), ExtendsType = typeof(Resource), DiscriminatorValue = "Menu")]
    public class Menu : Resource
    {
        /// <summary>
        /// Web 菜单图标
        /// </summary>
        [Property(Column = "Icon", Length = 100)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Web 菜单链接
        /// </summary>
        [Property(Column = "Url", Length = 100)]
        public virtual string Url { get; set; }

        /// <summary>
        /// WinForm 菜单图片
        /// </summary>
        [Property(Column = "Image", Length = 100)]
        public virtual string Image { get; set; }

        /// <summary>
        /// WinForm窗体名称
        /// </summary>
        [Property(Column = "FrmName", Length = 100)]
        public virtual string FrmName { get; set; }

        /// <summary>
        /// WinForm窗体类型
        /// </summary>
        [Property(Column = "IsMdiFrm", Length = 100)]
        public virtual bool IsMdiFrm { get; set; }


        #region  多级菜单构建
        [Property(Column = "ParentId")]
        public virtual Guid? ParentId { get; set; }

        //[JsonIgnore]
        //[ManyToOne(Name = "Parent", Column = "ParentId", Insert = false, Update = false, ClassType = typeof(Menu))]
        //public virtual Menu Parent { get; set; }

        [JsonProperty(PropertyName = "children")]
        [Bag(1, Name = "Children", OrderBy = "OrderIndex", Cascade = "all-delete-orphan", Inverse = true, Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Subselect)]
        [Key(2, Column = "ParentId")]
        [OneToMany(3, ClassType = typeof(Menu))]
        public virtual IList<Menu> Children { get; set; }
        #endregion

        #region    Web仅用于EasyUI构建菜单
        [JsonProperty(PropertyName = "id")]
        protected Guid? id { get { return Id; } }

        [JsonProperty(PropertyName = "text")]
        protected string text { get { return Name; } }

        [JsonProperty(PropertyName = "iconCls")]
        protected string iconCls { get { return Icon; } }

        [JsonProperty(PropertyName = "url")]
        protected string url { get { return Url; } }

        [JsonProperty(PropertyName = "_parentId")]
        protected Guid? parentId { get { return ParentId; } }
        #endregion

    }

    /// <summary>
    /// 报表
    /// </summary>
    [Subclass(NameType = typeof(Report), ExtendsType = typeof(Resource), DiscriminatorValue = "Report")]
    public class Report : Resource
    {

    }
}
