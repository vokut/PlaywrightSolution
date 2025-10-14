using Microsoft.Playwright;
using Playwright.Core.Utilities;

namespace Playwright.Core.Drivers
{
    public static class PlaywrightDriver
    {
        public static async Task<BrowserSession> InitializePlaywrightAsync()
        {
            var cfg = ConfigManager.Config;

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browserType = cfg.Browser.ToLower() switch
            {
                "firefox" => playwright.Firefox,
                "webkit" => playwright.Webkit,
                _ => playwright.Chromium
            };

            var browser = await browserType.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = cfg.Headless
            });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = cfg.BrowserWidth,
                    Height = cfg.BrowserHeight
                },
            });

            var page = await context.NewPageAsync();

            
            page.SetDefaultTimeout(cfg.Timeout);                  
            page.SetDefaultNavigationTimeout(cfg.NavigationTimeout);

            //wait-for selectors, assertions, etc., globally:
            context.SetDefaultTimeout(cfg.Timeout);

            return new BrowserSession(playwright, browser, context, page);
        }


        public class BrowserSession
        {
            public IPlaywright Playwright { get; }
            public IBrowser Browser { get; }
            public IBrowserContext Context { get; }
            public IPage Page { get; }

            public BrowserSession(IPlaywright playwright, IBrowser browser, IBrowserContext context, IPage page)
            {
                Playwright = playwright;
                Browser = browser;
                Context = context;
                Page = page;
            }

            public async Task CloseAsync()
            {
                //Console.WriteLine($"Playwright Hash: {playwright.GetHashCode()}");
                //Console.WriteLine($"Browser Hash:    {browser.GetHashCode()}");
                try
                {
                    if (Browser != null && Browser.IsConnected)
                        await Browser.CloseAsync();
                }
                finally
                {
                    Playwright?.Dispose();
                }
            }

        }

        public static async Task CloseAsync(IPlaywright playwright, IBrowser browser)
        {
            //Console.WriteLine($"Playwright Hash: {playwright.GetHashCode()}");
            //Console.WriteLine($"Browser Hash:    {browser.GetHashCode()}");
            try
            {
                if (browser != null && browser.IsConnected)
                    await browser.CloseAsync();
            }
            finally
            {
                playwright?.Dispose();
            }
        }
    }
}

