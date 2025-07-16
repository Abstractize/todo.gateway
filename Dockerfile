# syntax=docker/dockerfile:1.4

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG GITHUB_USERNAME

WORKDIR /src
COPY src .

WORKDIR /src/API

RUN --mount=type=secret,id=GITHUB_TOKEN \
    GITHUB_TOKEN=$(cat /run/secrets/GITHUB_TOKEN) && \
    cat <<EOF > /src/API/nuget.config
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
    <packageSources>
        <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
        <add key="github" value="https://nuget.pkg.github.com/${GITHUB_USERNAME}/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
        <add key="Username" value="${GITHUB_USERNAME}" />
        <add key="ClearTextPassword" value="${GITHUB_TOKEN}" />
        </github>
    </packageSourceCredentials>
    </configuration>
    EOF

RUN dotnet restore --configfile /src/API/nuget.config
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]