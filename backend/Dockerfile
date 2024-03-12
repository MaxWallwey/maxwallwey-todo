FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY backend/api/src/Todo.Api/*.csproj backend/api/src/Todo.Api/

RUN dotnet restore backend/api/src/Todo.Api

COPY backend/api/src/Todo.Api/ backend/api/src/Todo.Api/

RUN dotnet publish backend/api/src/Todo.Api --configuration Release --output out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]