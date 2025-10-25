using Microsoft.Playwright;
using NUnit.Framework;

namespace Playwright.Core.Driver
{
    /// <summary>
    /// Handles global Playwright initialization. 
    /// Thread-safe, lightweight, and reused across all tests.
    /// </summary>
    public static class PlaywrightManager
    {
        private static readonly SemaphoreSlim _initLock = new(1, 1);
        private static IPlaywright _playwright;
        private static bool _initialized;

        public static IPlaywright Playwright => _playwright;

        public static async Task EnsureInitializedAsync()
        {
            if (_initialized) return; // fast path

            await _initLock.WaitAsync();
            try
            {
                if (_initialized) return; // double-check inside lock

                _playwright = await Microsoft.Playwright.Playwright.CreateAsync();
                _initialized = true;

                TestContext.Progress.WriteLine("Playwright initialized (thread-safe).");
            }
            finally
            {
                _initLock.Release();
            }
        }
    }
}
