using Microsoft.Playwright;
using NUnit.Framework;

namespace Playwright.Core.Drivers
{
    public static class PlaywrightManager
    {
        private static readonly Lazy<Task<IPlaywright>> _lazyPlaywright = new(
            InitializePlaywrightInternalAsync, LazyThreadSafetyMode.ExecutionAndPublication
        );

        public static Task<IPlaywright> GetPlaywrightAsync()
        {
            return _lazyPlaywright.Value;
        }

        private static async Task<IPlaywright> InitializePlaywrightInternalAsync()
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            TestContext.Progress.WriteLine("Playwright initialized (thread-safe).");
            return playwright;
        }
    }
}
