using System;

namespace RiverLi.DDD.Core.Domain.Common
{
    /// <summary>
    /// 审计接口：定义创建/更新信息的标准契约
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        string? Creator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        string? Updator { get; set; }
    }
}