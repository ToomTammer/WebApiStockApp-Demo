# Creating xUnit Tests for ASP.NET Core 7.0 Web API with C# in VSCode

## 1. Set Up Your ASP.NET Core Web API Project
1. Open a terminal in VSCode.
2. Create a new Web API project:
   ```bash
   dotnet new webapi -n webapi
   cd webapi

## 2. Add xUnit Testing Project
1. Navigate to the root of your solution
    ```bash
    cd ..
2. Create a new xUnit test project
     ```bash
    dotnet new xunit -n webapi.Tests
    cd webapi.Tests
3. Add the test project to the solution
    ```bash
    dotnet sln add webapi.Tests/webapi.Tests.csproj
    or add this in test project
    <ItemGroup>
    <ProjectReference Include="..\webapi\webapi.csproj" />
    </ItemGroup>
## 3. Add Reference to ASP.NET Core Project
1. Add a reference to the ASP.NET Core project
    ```bash
    dotnet add webapi.Tests.csproj reference ../webapi/webapi.csproj
## 4. Install xUnit and Testing Packages
1. Install xUnit and other necessary packages
    ```bash
    dotnet add webapi.Tests package xunit
    dotnet add webapi.Tests package xunit.runner.visualstudio
    dotnet add webapi.Tests package Moq
## 5. Write Your First Test
1. Create a test class, e.g., 'WeatherForecastControllerTests.cs'

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace webapi.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly HttpClient _client;

        public WeatherForecastControllerTests()
        {
            _client = new HttpClient();
        }

        [Fact]
        public async Task Get_ReturnsSuccessStatusCode()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/weatherforecast");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}

## 6. Run Your Tests
    ``bash
    dotnet test

## 7. Debug Tests in VSCode
1. Create or modify '.vscode/launch.json'
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (xUnit Tests)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/webapi.Tests/bin/Debug/net7.0/webapi.Tests.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false
        }
    ]
}

