CashFlow Solution
This project follows a clean-architecture layout for .NET and was generated entirely with the .NET CLI.
It contains separate projects for API, Application logic, Domain, Infrastructure, Communication, and Exception handling.

📁 Solution structure
CashFlow
├── src/
│   ├── CashFlow.Api
│   ├── CashFlow.Application
│   ├── CashFlow.Communication
│   ├── CashFlow.Domain
│   ├── CashFlow.Exception
│   ├── CashFlow.Infra
└── tests/

🛠 Commands used
1 Create an empty (blank) solution
mkdir CashFlow
cd CashFlow
dotnet new sln --name CashFlow


2 Create the projects
dotnet new webapi   -n CashFlow.Api           -o src/CashFlow.Api
dotnet new classlib -n CashFlow.Application   -o src/CashFlow.Application
dotnet new classlib -n CashFlow.Communication -o src/CashFlow.Communication
dotnet new classlib -n CashFlow.Domain        -o src/CashFlow.Domain
dotnet new classlib -n CashFlow.Exception     -o src/CashFlow.Exception
dotnet new classlib -n CashFlow.Infra         -o src/CashFlow.Infra


3 Add the projects to the solution
dotnet sln add src/CashFlow.Api/CashFlow.Api.csproj
dotnet sln add src/CashFlow.Application/CashFlow.Application.csproj
dotnet sln add src/CashFlow.Communication/CashFlow.Communication.csproj
dotnet sln add src/CashFlow.Domain/CashFlow.Domain.csproj
dotnet sln add src/CashFlow.Exception/CashFlow.Exception.csproj
dotnet sln add src/CashFlow.Infra/CashFlow.Infra.csproj

4 (Optional) create a test project

mkdir tests
dotnet new xunit -n CashFlow.Tests -o tests/CashFlow.Tests
dotnet sln add tests/CashFlow.Tests/CashFlow.Tests.csproj





