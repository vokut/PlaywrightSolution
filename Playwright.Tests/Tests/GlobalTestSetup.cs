using Playwright.Core.Models;
using Playwright.Core.Utilities;

namespace Playwright.Tests
{
    [SetUpFixture]
    public class GlobalTestSetup
    {
        [OneTimeSetUp]
        public void GlobalInitialize()
        {
            var searchWord = TestContext.Parameters.Get("SearchWord", "hello");
            var baseUrl = TestContext.Parameters.Get("BaseUrl", "https://default.com");
            var browser = TestContext.Parameters.Get("Browser", "chromium");
            var headless = bool.Parse(TestContext.Parameters.Get("Headless", "true"));
            var width = int.Parse(TestContext.Parameters.Get("BrowserWidth", "1920"));
            var height = int.Parse(TestContext.Parameters.Get("BrowserHeight", "1080"));
            var timeout = int.Parse(TestContext.Parameters.Get("Timeout", "30000"));
            var navTimeout = int.Parse(TestContext.Parameters.Get("NavigationTimeout", "30000"));
            var actionTimeout = int.Parse(TestContext.Parameters.Get("ActionTimeout", "10000"));

            ConfigManager.Initialize(new ConfigModel
            {
                SearchWord = searchWord,
                BaseUrl = baseUrl,
                Browser = browser,
                Headless = headless,
                BrowserWidth = width,
                BrowserHeight = height,
                Timeout = timeout,
                NavigationTimeout = navTimeout,
                ActionTimeout = actionTimeout
            });
        }
    }
}
