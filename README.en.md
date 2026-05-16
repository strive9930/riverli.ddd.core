# RiverLi.DDD.Core

Overview

Core DDD building blocks for microservices: BaseEntity, domain events, repository/unit-of-work interfaces, and common application interfaces (ICacheService, ICurrentUser).

Quick start

- Reference the package and inherit entities from `BaseEntity`.
- Publish domain events via `IDomainEventPublisher`.

NuGet & Build

```bash
dotnet build -c Release
dotnet pack -c Release
dotnet nuget push bin/Release/*.nupkg -k <API_KEY> -s <NUGET_URL>
```
