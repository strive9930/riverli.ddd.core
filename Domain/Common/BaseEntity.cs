using System;
using System.ComponentModel.DataAnnotations;
using RiverLi.DDD.Core.Domain.Events;
using System.Text.Json.Serialization;
namespace RiverLi.DDD.Core.Domain.Common
{
    /// <summary>
    /// 领域实体通用基类，实现IEntity、软删除和审计能力
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseEntity<TKey> : IEntity<TKey>, ISoftDelete, IAuditableEntity 
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 实体唯一主键
        /// </summary>
        public TKey Id { get; set; } = default!;

        #region IAuditableEntity 实现 (使用你的命名)

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? Creator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? Updator { get; set; }

        #endregion

        #region ISoftDelete 实现

        /// <summary>
        /// 删除状态
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 删除时间（UTC），未删除时为null
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        
        [Timestamp] // EF Core并发令牌
        public byte[]? RowVersion { get; set; }
        
        #endregion

        protected BaseEntity()
        {
            CreateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 更新实体的更新时间为当前UTC时间
        /// </summary>
        public virtual void UpdateModifyTime()
        {
            UpdateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 标记实体为删除状态（软删除）
        /// </summary>
        /// <param name="operatorId">操作人ID(可选)</param>
        public virtual void MarkAsDeleted(string? operatorId = null)
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeleteTime = DateTime.UtcNow;
            
            // 删除同时也算一次更新
            UpdateTime = DateTime.UtcNow;
            if (operatorId != null)
            {
                Updator = operatorId; 
            }
        }

        #region DDD 核心：基于Id的相等性 (建议保留)

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity<TKey> other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (Id.Equals(default(TKey)) || other.Id.Equals(default(TKey))) return false;
            
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<TKey> left, BaseEntity<TKey> right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<TKey> left, BaseEntity<TKey> right)
        {
            return !(left == right);
        }

        #endregion
        #region Domain Events 

        // 使用私有字段存储，防止外部随意修改
        private List<IDomainEvent>? _domainEvents;

        // 公开只读集合供 Infrastructure 层获取
        // 注意：JsonIgnore 防止该数据被序列化返回给 API 调用方
        [JsonIgnore] 
        public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

        /// <summary>
        /// 添加领域事件
        /// </summary>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// 移除领域事件
        /// </summary>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        /// <summary>
        /// 清空领域事件
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion
    }

    /// <summary>
    /// 主键为Guid的通用实体基类
    /// </summary>
    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}