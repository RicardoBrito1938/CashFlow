
# CashFlow Application
This project is architected using clean architecture principles in .NET 9.0 with a modular design separating API, Application, Domain, Infrastructure, Communication, and Exception handling layers. It leverages MySQL as the database backend, with Docker support for easy setup and deployment. The codebase emphasizes maintainable and testable design, incorporating robust unit testing using xUnit alongside powerful tools such as AutoMapper for object mapping, FluentAssertions for expressive and readable tests, FluentValidation for comprehensive request data validation, and Scallar for functional programming paradigms or scripting integration. These technologies help ensure clean, reliable, and scalable application development.
## Database Setup

To create the MySQL database for this application, run the following Docker command:

```bash
docker run --name cashflow -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 -d mysql:latest
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





