FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /backend
COPY zscalar.crt .
ADD zscalar.crt /usr/local/share/ca-certificates/zscalar.crt
ADD zscalar.crt /etc/ssl/certs/zscalar.crt
RUN chmod 644 /usr/local/share/ca-certificates/zscalar.crt && update-ca-certificates

WORKDIR /src
COPY ["src/Todo.Api/Todo.Api.csproj", "src/Todo.Api/"]
RUN dotnet restore "src/Todo.Api/Todo.Api.csproj"

COPY . .
RUN dotnet build "src/Todo.Api/Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Todo.Api/Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]