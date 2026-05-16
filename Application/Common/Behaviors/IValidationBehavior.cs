using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverLi.DDD.Core.Application.Common.Behaviors
{
    /// <summary>
    /// MediatR验证管道抽象接口
    /// 所有微服务的Command/Query验证逻辑统一实现此接口，适配MediatR管道机制
    /// 用于CQS模式中，执行处理程序前先进行参数验证
    /// </summary>
    /// <typeparam name="TRequest">MediatR请求类型（ICommand/IQuery）</typeparam>
    /// <typeparam name="TResponse">返回结果类型（通常为Result/Result<T>）</typeparam>
    public interface IValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// 验证处理方法
        /// </summary>
        /// <param name="request">请求实例</param>
        /// <param name="next">下一个管道处理程序</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>处理结果</returns>
        new Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }
}
