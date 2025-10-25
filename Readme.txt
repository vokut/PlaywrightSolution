dotnet test --settings nunit.runsettings -- NUnit.NumberOfTestWorkers=5

allure generate Playwright.Tests\bin\Debug\net9.0\allure-results -o allure-report --clean
allure open allure-report