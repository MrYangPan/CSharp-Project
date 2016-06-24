using System;
using Newtonsoft.Json;
using NHibernate.Mapping.Attributes;

namespace Richnova.CEMS.Framework.Data
{
    /// <summary>
    /// 支持NHibernate专用领域实体基类
    /// </summary>
    public abstract class NHibernateEntity
    {
        #region Properties
        /// <summary>
        /// 主键
        /// </summary>
        [Id(1, Name = "Id", TypeType = typeof(Guid), Column = "Id")]
        [Generator(2, Class = "guid.comb")]
        public virtual Guid? Id { get; set; }

        /// <summary>
        /// 并发版本控制
        /// </summary>
        [Version(UnsavedValue = "0")]
        public virtual int Version { get; set; }

        /// <summary>
        /// 实例编码
        /// </summary>
        [JsonIgnore]
        [Property(Column = "Instance", Length = 36)]
        public virtual string Instance { get; set; }

        /// <summary>
        /// 数据状态（新增，编辑，废弃）
        /// </summary>
        [JsonIgnore]
        [Property(Column = "IsDeleted")]
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Property(Column = "CreatedBy", Length = 36)]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property(Column = "CreatedAt")]
        public virtual DateTime? CreatedAt { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Property(Column = "UpdatedBy", Length = 36)]
        public virtual string UpdatedBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Property(Column = "UpdatedAt")]
        public virtual DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        [Property(Column = "DeletedBy", Length = 36)]
        public virtual string DeletedBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Property(Column = "DeletedAt")]
        public virtual DateTime? DeletedAt { get; set; }
        #endregion
    }
}