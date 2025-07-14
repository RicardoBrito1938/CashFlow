FROM mrc.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

COPY src/ .

WORKDIR /app/Cashflow.APi

RUN dotnet restore

RUN dotnet publish -c Release -o /app/out

FROM mrc.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "Cashflow.Api.dll" ]