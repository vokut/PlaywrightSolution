using Microsoft.Playwright;
using Playwright.Core.Config;
using Playwright.Core.Drivers;
using Playwright.Core.Models;
using Playwright.Tests.Base;
using Reqnroll;

namespace Playwright.Reqnroll.Hooks
{
    [Binding]
    public class PlaywrightHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private PlaywrightDriver _driver = null!;
        private PageManager _pageManager = null!;

        private static TestSettings _baseSettings = null!;
        private static IPlaywright _basePlaywright = null!;

        public PlaywrightHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            _baseSettings = ConfigManager.Initialize();
            _basePlaywright = await PlaywrightManager.GetPlaywrightAsync();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _driver = new PlaywrightDriver(_basePlaywright, _baseSettings);
            await _driver.InitializeAsync();

            _pageManager = new PageManager(_driver.Page);

            // make everything accessible in steps
            _scenarioContext["Driver"] = _driver;
            _scenarioContext["PageManager"] = _pageManager;
            _scenarioContext["Page"] = _driver.Page;
            _scenarioContext["Context"] = _driver.Context;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_driver != null)
                await _driver.DisposeAsync();
        }
    }
}
