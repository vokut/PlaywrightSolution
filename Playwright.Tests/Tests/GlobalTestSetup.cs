using Allure.Commons;
using dotenv.net;
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
            AllureLifecycle.Instance.CleanupResultDirectory();

            // Go up until we find a .sln or .env file, whichever comes first
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && !File.Exists(Path.Combine(dir.FullName, ".env")) && !dir.GetFiles("*.sln").Any())
            {
                dir = dir.Parent;
            }

            if (dir == null)
            {
                throw new DirectoryNotFoundException("Could not locate solution directory or .env file.");
            }

            var envPath = Path.Combine(dir.FullName, ".env");

            if (File.Exists(envPath))
            {
                DotEnv.Load(new DotEnvOptions(envFilePaths: new[] { envPath }, overwriteExistingVars: false));

            }

            var searchWord = TestContext.Parameters.Get("SearchWord", "hello");
            var baseUrl = TestContext.Parameters.Get("BaseUrl", "https://default.com");
            var browser = TestContext.Parameters.Get("Browser", "chromium");
            var headless = bool.Parse(TestContext.Parameters.Get("Headless", "true"));
            var width = int.Parse(TestContext.Parameters.Get("BrowserWidth", "1920"));
            var height = int.Parse(TestContext.Parameters.Get("BrowserHeight", "1080"));
            var timeout = int.Parse(TestContext.Parameters.Get("Timeout", "30000"));
            var navTimeout = int.Parse(TestContext.Parameters.Get("NavigationTimeout", "30000"));
            var actionTimeout = int.Parse(TestContext.Parameters.Get("ActionTimeout", "10000"));
            var orangeUrl = Environment.GetEnvironmentVariable("ORANGEHRM_URL") ?? "";
            var orangeUser = Environment.GetEnvironmentVariable("ORANGEHRM_USER") ?? "";
            var orangePass = Environment.GetEnvironmentVariable("ORANGEHRM_PASSWORD") ?? "";

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
                ActionTimeout = actionTimeout,
                OrangeHrmUrl = orangeUrl,
                OrangeHrmUser = orangeUser,
                OrangeHrmPassword = orangePass
            });
        }
    }
}
