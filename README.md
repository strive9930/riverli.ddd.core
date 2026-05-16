# RiverLi.DDD.Core  (核心领域类库)

[![build](https://img.shields.io/badge/build-passing-brightgreen)](https://example.com)

**概述**
包含 DDD 常用基类与接口，便于在微服务中统一领域模型与基础行为：领域事件、实体基类、仓储接口、审计字段与软删除等。

**快速上手**
- 引用 `RiverLi.DDD.Core`，在实体继承 `BaseEntity`，并在领域操作完成后通过 `IDomainEventPublisher` 发布事件。

**示例**
```csharp
public class Post : BaseEntity { public string Title { get; set; } }

// 注入仓储
public MyService(IRepository<Post, Guid> repo) { }
```

**注意**
此库为框架层，尽量保持轻量，避免引入大量外部依赖。

## NuGet / Usage
Pack & publish:
```bash
dotnet pack -c Release
dotnet nuget push bin/Release/*.nupkg -k <API_KEY> -s <NUGET_URL>
```
Usage example:
```xml
<PackageReference Include="RiverLi.DDD.Core" Version="1.0.0" />
```