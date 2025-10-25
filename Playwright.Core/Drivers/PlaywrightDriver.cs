using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core.Config;
using Playwright.Core.Models;

namespace Playwright.Core.Driver
{
    /// <summary>
    /// Creates and manages a dedicated browser, context, and page per test.
    /// </summary>
    public class PlaywrightDriver : IAsyncDisposable
    {
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IBrowserContext _context = null!;
        private IPage _page = null!;

        public IBrowser Browser => _browser;
        public IBrowserContext Context => _context;
        public IPage Page => _page;

        public async Task InitializeAsync()
        {
            await PlaywrightManager.EnsureInitializedAsync();
            _playwright = PlaywrightManager.Playwright;

            var pw = ConfigManager.Settings.Playwright;
            var browserName = pw.Browser.ToLower();

            _browser = browserName switch
            {
                "firefox" => await _playwright.Firefox.LaunchAsync(new() { Headless = pw.Headless }),
                "webkit" => await _playwright.Webkit.LaunchAsync(new() { Headless = pw.Headless }),
                _ => await _playwright.Chromium.LaunchAsync(new() { Headless = pw.Headless })
            };

            var options = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = pw.Viewport.Width, Height = pw.Viewport.Height },
                DeviceScaleFactor = (float?)pw.Viewport.DeviceScaleFactor
            };

            _context = await _browser.NewContextAsync(options);

            _page = await _context.NewPageAsync();
            _context.SetDefaultTimeout(pw.DefaultTimeout);
            _context.SetDefaultNavigationTimeout(pw.NavigationTimeout);

            TestContext.Progress.WriteLine($"Browser launched: {browserName}, headless={pw.Headless}");
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (ConfigManager.Settings?.Playwright.RecordTrace == true && _context != null)
                {
                    //TO DO
                }

                if (_context != null)
                    await _context.CloseAsync();

                if (_browser != null)
                    await _browser.CloseAsync();
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Cleanup error: {ex.Message}");
            }
        }
    }
}
