FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /backend
COPY zscalar.crt .
ADD zscalar.crt /usr/local/share/ca-certificates/zscalar.crt
ADD zscalar.crt /etc/ssl/certs/zscalar.crt
RUN chmod 644 /usr/local/share/ca-certificates/zscalar.crt && update-ca-certificates

WORKDIR /src
COPY ["Todo.Identity/Todo.Identity.csproj", "Todo.Identity/"]
RUN dotnet restore "Todo.Identity/Todo.Identity.csproj"
COPY . .
WORKDIR "/src/Todo.Identity"
RUN dotnet build "Todo.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todo.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Identity.dll"]
