using Microsoft.Playwright;
using Playwright.Core.Config;
using Playwright.Core.Driver;
using Playwright.Core.Models;

namespace Playwright.Tests.Base
{
    public abstract class TestBase
    {
        protected PlaywrightDriver Driver;
        protected IBrowserContext Context;
        protected IPage Page;
        protected PageManager PageManager { get; private set; }
        protected TestSettings Settings => ConfigManager.Settings;

        [SetUp]
        public async Task SetupAsync()
        {
            Driver = new PlaywrightDriver();
            await Driver.InitializeAsync(); // this handles all setup internally
            PageManager = new PageManager(Driver.Page);
            Context = Driver.Context;
            Page = Driver.Page;
        }

        [TearDown]
        public async Task TearDownAsync() => await Driver.DisposeAsync();

        //[OneTimeTearDown]
        //public static async Task GlobalTeardownAsync() => await PlaywrightManager.DisposeAsync();
    }
}