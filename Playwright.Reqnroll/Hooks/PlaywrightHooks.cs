using Playwright.Core.Driver;
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

        public PlaywrightHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _driver = new PlaywrightDriver();
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
