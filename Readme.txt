dotnet test --settings nunit.runsettings -- NUnit.NumberOfTestWorkers=5

$env:TEST_ENVIRONMENT="prod"; dotnet test --settings nunit.runsettings

$env:TEST_ENVIRONMENT="dev"; dotnet test -v n Playwright.Tests/Playwright.Tests.csproj -- NUnit.NumberOfTestWorkers=6 --settings nunit.runsettings


$env:TEST_ENVIRONMENT="dev"; dotnet test -v n Playwright.Reqnroll/Playwright.Reqnroll.csproj -- NUnit.NumberOfTestWorkers=6 --settings nunit.runsettings


allure generate Playwright.Tests\bin\Debug\net9.0\allure-results -o allure-report --clean
allure open allure-report