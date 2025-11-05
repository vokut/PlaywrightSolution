using Microsoft.Playwright;
using Playwright.Core.Config;
using Playwright.Core.Drivers;
using Playwright.Core.Models;
using Playwright.Tests.Base;

namespace Playwright.Tests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public async Task RunBeforeAllTests()
        {
            //Initialize config
            TestSettings settings = ConfigManager.Initialize();

            // Initialize Playwright
            IPlaywright playwright = await PlaywrightManager.GetPlaywrightAsync();

            // Store both dependencies in static TestBase fields
            TestBase.SetBaseDependencies(settings, playwright);
        }
    }
}
