using System.Web;
using Microsoft.Playwright;

namespace Playwright.Tests.Pages
{
    public class BingPage: PageBase
    {
        public BingPage(IPage page) : base(page) { }

        private ILocator SearchBox => _page.Locator("[name='q']");
        private ILocator AcceptButton => _page.Locator("#bnp_btn_accept");

        public async Task AcceptCookiesIfPresentAsync()
        {
            await Task.Delay(500); //just for the sake of clicking the button
            if (await AcceptButton.IsVisibleAsync())
                await AcceptButton.ClickAsync();
        }

        public async Task SearchAsync(string query)
        {
            await SearchBox.FillAsync(query);
            await SearchBox.PressAsync("Enter");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Assert.That(HttpUtility.UrlDecode(_page.Url), Does.Contain("bing.com/search?q="));
        }

    }
}
