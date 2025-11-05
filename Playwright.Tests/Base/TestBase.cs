using Microsoft.Playwright;
using Playwright.Core.Config;
using Playwright.Core.Drivers;
using Playwright.Core.Models;

namespace Playwright.Tests.Base
{
    public abstract class TestBase
    {
        protected PlaywrightDriver Driver;
        protected PageManager PageManager { get; private set; }

        protected readonly TestSettings Settings;
        protected readonly IPlaywright Playwright;

        private static TestSettings _baseSettings = null!;
        private static IPlaywright _basePlaywright = null!;

        internal static void SetBaseDependencies(TestSettings settings, IPlaywright playwright)
        {
            _baseSettings = settings;
            _basePlaywright = playwright;
        }

        protected TestBase() : this(_baseSettings, _basePlaywright)
        {
        }

        protected TestBase(TestSettings settings, IPlaywright playwright)
        {
            Settings = settings;
            Playwright = playwright;
        }

        [SetUp]
        public async Task SetupAsync()
        {
            Driver = new PlaywrightDriver(Playwright, Settings);
            await Driver.InitializeAsync();

            PageManager = new PageManager(Driver.Page);
        }

        [TearDown]
        public async Task TearDownAsync() => await Driver.DisposeAsync();
    }
}