FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

# copy csproj and restore as distinct layers
COPY *.sln .
COPY LightNote.Api/*.csproj ./LightNote.Api/
COPY LightNote.Application/*.csproj ./LightNote.Application/
COPY LightNote.Dal/*.csproj ./LightNote.Dal/
COPY LightNote.Domain/*.csproj ./LightNote.Domain/
COPY LightNote.UnitTests/*.csproj ./LightNote.UnitTests/
COPY LightNote.IntegrationTests/*.csproj ./LightNote.IntegrationTests/


# copy everything else and build app
COPY LightNote.Api/. ./LightNote.Api/
COPY  LightNote.Application/. ./LightNote.Application/
COPY  LightNote.Dal/. ./LightNote.Dal/
COPY  LightNote.Domain/. ./LightNote.Domain/
COPY  LightNote.Dal/. ./LightNote.Dal/
COPY  LightNote.UnitTests/. ./LightNote.UnitTests/
COPY  LightNote.IntegrationTests/. ./LightNote.IntegrationTests/
RUN dotnet restore

WORKDIR /app/LightNote.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

COPY --from=build /app/LightNote.Api/out ./
ENTRYPOINT ["dotnet", "LightNote.Api.dll"]