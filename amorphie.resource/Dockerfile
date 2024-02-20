FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN adduser -u 5786 --disabled-password --gecos "" amorphie-resource && chown -R amorphie-resource:amorphie-resource /app
USER amorphie-resource

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["/amorphie.resource/amorphie.resource.csproj", "."]
COPY ["/amorphie.resource.data/amorphie.resource.data.csproj", "."]
RUN dotnet restore "./amorphie.resource.csproj"

COPY . .

WORKDIR "/src/."
RUN dotnet build "./amorphie.resource/amorphie.resource.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./amorphie.resource/amorphie.resource.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "amorphie.resource.dll"]
