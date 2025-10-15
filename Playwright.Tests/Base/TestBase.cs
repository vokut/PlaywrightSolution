using static Playwright.Core.Drivers.PlaywrightDriver;

namespace Playwright.Tests.Base
{
    [TestFixture]
    public abstract class TestBase
    {
        protected PageManager PageManager { get; private set; }
        protected BrowserSession Session { get; private set; }

        [SetUp]
        public async Task BaseSetup()
        {
            Session = await InitializePlaywrightAsync();
            PageManager = new PageManager(Session.Page);
        }

        [TearDown]
        public async Task BaseTeardown()
        {
            if (Session != null)
            {
                await Session.CloseAsync();
            }
            await Task.Delay(100); // let browser exit gracefully
        }
    }
}
