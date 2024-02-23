FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Todo.API/Todo.API.csproj", "src/Todo.API/"]
COPY ["src/Todo.Cli/Todo.Cli.csproj", "src/Todo.Cli/"]
RUN dotnet restore "src/Todo.API/Todo.API.csproj"
COPY . .
WORKDIR "/src/src/Todo.API"
RUN dotnet build "Todo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.API.dll"]
