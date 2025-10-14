using Microsoft.Playwright;

namespace Playwright.Tests.Pages
{
    public abstract class PageBase
    {
        protected readonly IPage _page;

        protected PageBase(IPage page)
        {
            _page = page;
        }

        public virtual async Task NavigateAsync(string url)
        {
            try
            {
                await _page.GotoAsync(url, new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                });
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine($"[Navigation ERROR] {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetTitleAsync() => await _page.TitleAsync();


    }
}
