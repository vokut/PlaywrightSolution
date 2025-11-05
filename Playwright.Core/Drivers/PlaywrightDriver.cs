using Microsoft.Playwright;
using NUnit.Framework;
using Playwright.Core.Config;
using Playwright.Core.Models;
using System.Diagnostics;

namespace Playwright.Core.Drivers
{
    public class PlaywrightDriver
    {
        private readonly IPlaywright _playwright;
        private readonly TestSettings _settings;

        private IBrowser _browser = null!;
        private IBrowserContext _context = null!;
        private IPage _page = null!;

        public IBrowser Browser => _browser;
        public IBrowserContext Context => _context;
        public IPage Page => _page;

        public PlaywrightDriver(IPlaywright playwright, TestSettings settings)
        {
            _playwright = playwright;
            _settings = settings;
        }

        public async Task InitializeAsync()
        {
            var pw = _settings.Playwright;
            var browserName = pw.Browser.ToLower();

            var chromiumArgs = new List<string>
            {
                "--disable-renderer-backgrounding",
                "--disable-background-timer-throttling",
                "--disable-backgrounding-occluded-windows",
                "--disable-features=PaintHolding",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--mute-audio"
            };

            if (pw.Headless)
            {
                chromiumArgs.Add("--use-gl=angle");
                //chromiumArgs.Add("--enable-webgl");
                //chromiumArgs.Add("--ignore-gpu-blocklist");
                //chromiumArgs.Add("--disable-animations");
            }

            var chromiumLaunchOptions = new BrowserTypeLaunchOptions
            {
                Headless = pw.Headless,
                Args = chromiumArgs.ToArray()
            };

            var defaultLaunchOptions = new BrowserTypeLaunchOptions { Headless = pw.Headless };

            var devicePreset = Environment.GetEnvironmentVariable("DEVICE_PRESET");

            DeviceConfig? selectedDevice = null;

            if (!string.IsNullOrEmpty(devicePreset) &&
                pw.Devices != null &&
                pw.Devices.TryGetValue(devicePreset, out var devCfg))
            {
                selectedDevice = devCfg;
            }

            var options = new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = pw.Viewport.Width, Height = pw.Viewport.Height },
                DeviceScaleFactor = (float?)pw.Viewport.DeviceScaleFactor
            };

            if (selectedDevice != null && _playwright.Devices.TryGetValue(selectedDevice.Device, out var device))
            {
                _browser = selectedDevice.Browser switch
                {
                    "firefox" => await _playwright.Firefox.LaunchAsync(defaultLaunchOptions),
                    "webkit" => await _playwright.Webkit.LaunchAsync(defaultLaunchOptions),
                    _ => await _playwright.Chromium.LaunchAsync(chromiumLaunchOptions)
                };

                options.ViewportSize = device.ViewportSize;
                options.DeviceScaleFactor = device.DeviceScaleFactor;
                options.IsMobile = device.IsMobile;
                options.UserAgent = device.UserAgent;
                options.HasTouch = device.HasTouch;

                TestContext.Progress.WriteLine($"Mobile emulation enabled: {selectedDevice.Device}, headless={pw.Headless}");
            }
            else
            {
                _browser = browserName switch
                {
                    "firefox" => await _playwright.Firefox.LaunchAsync(defaultLaunchOptions),
                    "webkit" => await _playwright.Webkit.LaunchAsync(defaultLaunchOptions),
                    _ => await _playwright.Chromium.LaunchAsync(chromiumLaunchOptions)
                };

                TestContext.Progress.WriteLine($"Desktop browser launched: {browserName}, headless={pw.Headless}");
            }

            _context = await _browser.NewContextAsync(options);

            _page = await _context.NewPageAsync();
            _context.SetDefaultTimeout(pw.DefaultTimeout);
            _context.SetDefaultNavigationTimeout(pw.NavigationTimeout);

        }

        public async Task DisposeAsync()
        {
            try
            {
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
