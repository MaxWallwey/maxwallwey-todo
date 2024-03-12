FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Todo.Api/Todo.Api.csproj", "src/Todo.Api/"]
COPY ["src/Todo.Api.Sdk/Todo.Api.Sdk.csproj", "src/Todo.Api.Sdk/"]
RUN dotnet restore "src/Todo.Api/Todo.Api.csproj"
COPY . .
WORKDIR "/src/src/Todo.Api"
RUN dotnet build "Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]
