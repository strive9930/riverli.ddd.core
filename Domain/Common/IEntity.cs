using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Domain.Common
{
    /// <summary>
    /// 实体通用接口，所有领域实体必须实现此接口
    /// </summary>
    /// <typeparam name="TKey">主键类型（Guid/long/string等）</typeparam>
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 实体唯一主键
        /// </summary>
        TKey Id { get; set; }
    }
}
