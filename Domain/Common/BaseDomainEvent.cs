using RiverLi.DDD.Core.Domain.Events;

namespace RiverLi.DDD.Core.Domain.Common;

/// <summary>
/// 领域事件抽象基类，自动生成事件ID和时间
/// </summary>
public abstract record BaseDomainEvent : IDomainEvent
{
    public DateTime EventTime { get; protected set; } = DateTime.UtcNow;
    public Guid EventId { get; protected set; } = Guid.NewGuid();
}