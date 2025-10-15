dotnet test --settings prod.runsettings

allure generate Playwright.Tests\bin\Debug\net9.0\allure-results -o allure-report --clean
allure open allure-report