dotnet test --settings nunit.runsettings -- NUnit.NumberOfTestWorkers=5

$env:TEST_ENVIRONMENT="prod"; dotnet test --settings nunit.runsettings

$env:TEST_ENVIRONMENT="dev"; dotnet test -v n Playwright.Tests/Playwright.Tests.csproj -- NUnit.NumberOfTestWorkers=6 --settings nunit.runsettings


$env:TEST_ENVIRONMENT="dev"; dotnet test -v n Playwright.Reqnroll/Playwright.Reqnroll.csproj -- NUnit.NumberOfTestWorkers=6 --settings nunit.runsettings

$env:TEST_ENVIRONMENT="dev";$env:DEVICE_PRESET="Pixel_7"; dotnet test Playwright.Tests/Playwright.Tests.csproj --filter "FullyQualifiedName=Playwright.Tests.AdminTests.CreateJobTitle" --settings nunit.runsettings

$env:TEST_ENVIRONMENT="dev";$env:DEVICE_PRESET=""; dotnet test Playwright.Tests/Playwright.Tests.csproj--settings nunit.runsettings

allure generate Playwright.Tests\bin\Debug\net9.0\allure-results -o allure-report --clean
allure open allure-report

1. Install scoop and Allure CLI
Set-ExecutionPolicy RemoteSigned -Scope CurrentUser;irm get.scoop.sh | iex
scoop install allure

2. Build solution and run this in PowerShell
<solution folder>\Spinberry.Tests\bin\Debug\net8.0\playwright.ps1 install

3.1 Run this in PowerShell selecting environment (dev or prod) and device (iPhone_15_Pro_Max,Pixel_7,iPad_Pro_11_Landscape
$env:TEST_ENVIRONMENT="dev";$env:DEVICE_PRESET=""; dotnet test Spinberry.Tests/Spinberry.Tests.csproj--settings nunit.runsettings

3.2 For Visual Studio have an .env file in the solution folder with the following contents
TEST_ENVIRONMENT=dev
#iPhone_15_Pro_Max,Pixel_7,iPad_Pro_11_Landscape
DEVICE_PRESET=
Framework__ORANGEHRM_URL=https://vokut-osondemand.orangehrm.com/auth/login

allure generate Spinberry.Tests\bin\Debug\net8.0\allure-results -o allure-report --clean
allure open allure-report
