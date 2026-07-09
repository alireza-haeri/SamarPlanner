FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SamarPlanner.Api/SamarPlanner.Api.csproj", "src/SamarPlanner.Api/"]
COPY ["src/Modules/SamarPlanner.Goal/SamarPlanner.Goal.csproj", "src/Modules/SamarPlanner.Goal/"]
COPY ["src/Shared/SamarPlanner.Shared.Contracts/SamarPlanner.Shared.Contracts.csproj", "src/Shared/SamarPlanner.Shared.Contracts/"]
COPY ["src/Shared/SamarPlanner.Shared.Kernel/SamarPlanner.Shared.Kernel.csproj", "src/Shared/SamarPlanner.Shared.Kernel/"]
COPY ["src/Shared/SamarPlanner.Shared.Infrastructure/SamarPlanner.Shared.Infrastructure.csproj", "src/Shared/SamarPlanner.Shared.Infrastructure/"]
COPY ["src/Shared/SamarPlanner.Shared/SamarPlanner.Shared.csproj", "src/Shared/SamarPlanner.Shared/"]
COPY ["src/Modules/SamarPlanner.Goal.Application/SamarPlanner.Goal.Application.csproj", "src/Modules/SamarPlanner.Goal.Application/"]
COPY ["src/Shared/SamarPlanner.Shared.Application/SamarPlanner.Shared.Application.csproj", "src/Shared/SamarPlanner.Shared.Application/"]
COPY ["src/Modules/SamarPlanner.Goal.Core/SamarPlanner.Goal.Core.csproj", "src/Modules/SamarPlanner.Goal.Core/"]
COPY ["src/Modules/SamarPlanner.Goal.Infrastructure/SamarPlanner.Goal.Infrastructure.csproj", "src/Modules/SamarPlanner.Goal.Infrastructure/"]
COPY ["src/Modules/SamarPlanner.Identity/SamarPlanner.Identity.csproj", "src/Modules/SamarPlanner.Identity/"]
COPY ["src/Modules/SamarPlanner.Identity.Core/SamarPlanner.Identity.Core.csproj", "src/Modules/SamarPlanner.Identity.Core/"]
COPY ["src/Modules/SamarPlanner.Identity.Infrastructure/SamarPlanner.Identity.Infrastructure.csproj", "src/Modules/SamarPlanner.Identity.Infrastructure/"]
COPY ["src/Modules/SamarPlanner.Note/SamarPlanner.Note.csproj", "src/Modules/SamarPlanner.Note/"]
COPY ["src/Modules/SamarPlanner.Note.Application/SamarPlanner.Note.Application.csproj", "src/Modules/SamarPlanner.Note.Application/"]
COPY ["src/Modules/SamarPlanner.Note.Core/SamarPlanner.Note.Core.csproj", "src/Modules/SamarPlanner.Note.Core/"]
COPY ["src/Modules/SamarPlanner.Note.Infrastructure/SamarPlanner.Note.Infrastructure.csproj", "src/Modules/SamarPlanner.Note.Infrastructure/"]
COPY ["src/Modules/SamarPlanner.Report/SamarPlanner.Report.csproj", "src/Modules/SamarPlanner.Report/"]
COPY ["src/Modules/SamarPlanner.Report.Application/SamarPlanner.Report.Application.csproj", "src/Modules/SamarPlanner.Report.Application/"]
COPY ["src/Modules/SamarPlanner.Report.Core/SamarPlanner.Report.Core.csproj", "src/Modules/SamarPlanner.Report.Core/"]
COPY ["src/Modules/SamarPlanner.Report.Infrastructure/SamarPlanner.Report.Infrastructure.csproj", "src/Modules/SamarPlanner.Report.Infrastructure/"]
COPY ["src/Modules/SamarPlanner.Task/SamarPlanner.Task.csproj", "src/Modules/SamarPlanner.Task/"]
COPY ["src/Modules/SamarPlanner.Task.Application/SamarPlanner.Task.Application.csproj", "src/Modules/SamarPlanner.Task.Application/"]
COPY ["src/Modules/SamarPlanner.Task.Core/SamarPlanner.Task.Core.csproj", "src/Modules/SamarPlanner.Task.Core/"]
COPY ["src/Modules/SamarPlanner.Task.Infrastructure/SamarPlanner.Task.Infrastructure.csproj", "src/Modules/SamarPlanner.Task.Infrastructure/"]
RUN dotnet restore "src/SamarPlanner.Api/SamarPlanner.Api.csproj"
COPY . .
WORKDIR "/src/src/SamarPlanner.Api"
RUN dotnet build "./SamarPlanner.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SamarPlanner.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SamarPlanner.Api.dll"]
