using System;
using System.Threading;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Application.Common.Interfaces
{
    /// <summary>
    /// 分布式缓存通用接口
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 获取缓存（泛型）
        /// </summary>
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间（null则不过期，建议必须传）</param>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 移除缓存
        /// </summary>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取或设置缓存（原子操作或封装逻辑）
        /// 如果缓存存在则返回，不存在则执行 factory 并缓存结果
        /// </summary>
        Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    }
}