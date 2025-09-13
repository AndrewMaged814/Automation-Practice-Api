# Automation Testing API

This project is a C# test automation framework for API testing, structured for modularity and maintainability. It is designed to test the Reqres API (https://reqres.in/) and can be extended for other APIs or UI scenarios.

## Features
- **API Testing**: Supports GET and POST requests, including handling of path, query, form, and multipart parameters.
- **Modular Design**: Uses interfaces and factories for REST methods, making it easy to extend or swap implementations.
- **Logging**: Custom logging with Serilog, including file and console sinks, and sensitive data masking.
- **Models**: Strongly-typed models for API responses and errors.
- **Utilities**: Helpers for assertions, configuration, and scenario context management.
- **Extensible**: Easily add new features, endpoints, or test scenarios.

## Project Structure
- `Common/` — Shared utilities, interfaces, logger, and REST method implementations.
- `Features/` — Test scenarios organized by API endpoint or feature.
- `Models/` — Data models for API requests and responses.
- `StepDefinitions/` — Step definitions for BDD-style tests.
- `Utils/` — Assertion helpers, configuration, and context utilities.

## Getting Started
1. **Clone the repository**
2. **Restore NuGet packages**
3. **Set the `BASE_URL` in `Common/Utils/Constants.cs`**
4. **Build the solution**
5. **Run tests** (using your preferred test runner)

## Logging
- Logs are written to `Common/Logs/` with daily rolling files.
- Sensitive information (like passwords and secrets) is masked in logs.

## Dependencies
- [Serilog](https://serilog.net/)
- [RestSharp](https://restsharp.dev/)
- [Xunit](https://xunit.net/) (for test output logging)

## Customization
- Add new API endpoints by extending the `Features/` and `Models/` folders.
- Implement new REST methods in `Common/RestMethods/` if needed.
- Update or add new step definitions in `StepDefinitions/`.

## License
This project is for educational and demonstration purposes.
