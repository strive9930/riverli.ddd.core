using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RiverLi.DDD.Core.Application.Common.Interfaces
{
    /// <summary>
    /// 当前用户上下文接口
    /// 解耦具体框架（如 ASP.NET Core），使领域/应用层能获取当前操作者信息
    /// </summary>
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        
        string? Id { get; }
        
        string? UserName { get; }
        
        string? Email { get; }

        /// <summary>
        /// 获取所有声明（可选，用于复杂权限判断）
        /// </summary>
        IEnumerable<Claim> Claims { get; }

        /// <summary>
        /// 泛型获取Id（方便转为 long, Guid 等不同类型）
        /// </summary>
        T GetUserId<T>();

        /// <summary>
        /// 检查是否拥有某个角色
        /// </summary>
        bool IsInRole(string role);
    }
}