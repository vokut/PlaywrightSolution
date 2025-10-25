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
        private readonly TestSettings _settings;

        public IBrowser Browser => _browser;
        public IBrowserContext Context => _context;
        public IPage Page => _page;
        public string ArtifactsDir => Path.Combine(TestContext.CurrentContext.WorkDirectory, "artifacts");

        public PlaywrightDriver()
        {
            // Ensure config is loaded once per test driver
            ConfigManager.Initialize();
            _settings = ConfigManager.Settings;
        }

        public async Task InitializeAsync()
        {
            await PlaywrightManager.EnsureInitializedAsync();
            _playwright = PlaywrightManager.Playwright;

            var pw = _settings.Playwright;
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

            if (pw.RecordTrace)
            {
                var traceDir = Path.Combine(ArtifactsDir, "Traces");
                Directory.CreateDirectory(traceDir);
                await _context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true, Sources = true });
            }

            _page = await _context.NewPageAsync();
            _context.SetDefaultTimeout(pw.DefaultTimeout);
            _context.SetDefaultNavigationTimeout(pw.NavigationTimeout);

            TestContext.Progress.WriteLine($"Browser launched: {browserName}, headless={pw.Headless}");
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_settings?.Playwright.RecordTrace == true && _context != null)
                {
                    var traceDir = Path.Combine(ArtifactsDir, "Traces");
                    Directory.CreateDirectory(traceDir);

                    var traceFile = Path.Combine(traceDir,
                        $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:HHmmss}.zip");

                    await _context.Tracing.StopAsync(new() { Path = traceFile });
                    TestContext.Progress.WriteLine($"Trace saved: {traceFile}");
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
