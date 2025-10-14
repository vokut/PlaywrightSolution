using Microsoft.Playwright;
using Playwright.Core.Drivers;
using Playwright.Core.Utilities;
using Playwright.Tests.Pages;
using static Playwright.Core.Drivers.PlaywrightDriver;

namespace Playwright.Tests.Tests
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class SearchTests
    {
        private  BrowserSession _session;
        private GooglePage _googlePage;
        private BingPage _bingPage;
        private readonly string _searchWord = ConfigManager.Config.SearchWord;

        [SetUp]
        public async Task Setup()
        {
            _session = await PlaywrightDriver.InitializePlaywrightAsync();
            _googlePage = new GooglePage(_session.Page);
            _bingPage = new BingPage(_session.Page);
        }

        [TearDown]
        public async Task Teardown()
        {
            await _session.CloseAsync();
        }

        [Test]
        //[Repeat(4)]
        public async Task Google()
        {
            await _googlePage.NavigateAsync("https://www.google.com/webhp?hl=en");

            await _googlePage.AcceptCookiesIfPresentAsync();
            await _googlePage.SearchAsync(_searchWord);

            //var title = await _googlePage.GetTitleAsync();
            //Assert.That(title.ToLower(), Does.Contain(_searchWord));
        }

        [Test]
        //[Repeat(4)]
        public async Task Bing()
        {
            await _bingPage.NavigateAsync("https://bing.com");
            await _bingPage.AcceptCookiesIfPresentAsync();
            await _bingPage.SearchAsync(_searchWord);
            //var title = await _bingPage.GetTitleAsync();
            //Assert.That(title.ToLower(), Does.Contain(_searchWord));
        }

        
    }
}
