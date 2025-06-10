
# CashFlow Application
This project is architected using clean architecture principles in .NET 9.0 with a modular design separating API, Application, Domain, Infrastructure, Communication, and Exception handling layers. It leverages MySQL as the database backend, with Docker support for easy setup and deployment. The codebase emphasizes maintainable and testable design, incorporating robust unit testing using xUnit alongside powerful tools such as AutoMapper for object mapping, FluentAssertions for expressive and readable tests, FluentValidation for comprehensive request data validation, and Scallar for functional programming paradigms or scripting integration. These technologies help ensure clean, reliable, and scalable application development.

## Features
- **Clean Architecture**: Modular design with clear separation of concerns.
- **.NET 9.0**: Utilizes the latest features and improvements in .NET.
- **MySQL Database**: Uses MySQL for data persistence, with Docker for easy setup.
- **Unit Testing**: Comprehensive unit tests using xUnit.
- **AutoMapper**: Simplifies object mapping between layers.
- **FluentAssertions**: Provides a more readable and expressive way to write assertions in tests.
- **FluentValidation**: Validates request data to ensure correctness and integrity.
- **Scallar**: Integrates functional programming paradigms or scripting capabilities.
- **Docker Support**: Simplifies deployment and environment setup.
- **Modular Design**: Each layer is encapsulated in its own project, promoting maintainability and scalability.
- **Exception Handling**: Centralized exception handling for better error management.
- **Communication Layer**: Facilitates communication between different parts of the application, such as APIs and services.
- **Domain Layer**: Contains business logic and domain entities, ensuring a clean separation from infrastructure concerns.
- **Infrastructure Layer**: Handles data access and external service integrations, keeping the domain layer clean and focused on business logic.
- **API Layer**: Exposes the application functionality through RESTful endpoints, adhering to best practices in API design.
- **Tests Layer**: Contains unit tests to ensure the reliability and correctness of the application.
- **Solution Structure**: Organized into a clear directory structure for easy navigation and understanding of the project.
- **Scalable and Maintainable**: Designed to be easily extendable and maintainable as the application grows.
- **Documentation**: Well-documented code and README for easy onboarding and understanding of the project.


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





