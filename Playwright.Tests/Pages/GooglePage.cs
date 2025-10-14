using System.Web;
using Microsoft.Playwright;

namespace Playwright.Tests.Pages
{
    public class GooglePage: PageBase
    {
        public GooglePage(IPage page) : base(page) { }

        private ILocator SearchBox => _page.Locator("[name='q']");
        private ILocator AcceptButton => _page.Locator("button:has-text('Accept all'), button:has-text('I agree')");

        public async Task AcceptCookiesIfPresentAsync()
        {
            if (await AcceptButton.IsVisibleAsync())
                await AcceptButton.ClickAsync();
        }

        public async Task SearchAsync(string query)
        {
            await SearchBox.FillAsync(query);
            await SearchBox.PressAsync("Enter");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Assert.That(HttpUtility.UrlDecode(_page.Url), Does.Contain("google.com/search?q="));
        }

    }
}
