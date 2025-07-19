FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# ✅ Copy the whole repo: includes .sln and src/
COPY . .

# ✅ Restore using the solution file
RUN dotnet restore CashFlow.sln

# ✅ Publish only the API project
RUN dotnet publish src/CashFlow.Api/CashFlow.Api.csproj -c Release -o /app/out

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "Cashflow.Api.dll" ]
