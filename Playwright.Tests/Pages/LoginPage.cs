using Microsoft.Playwright;
using Playwright.Core.Config;
using Playwright.Test.Pages;

namespace Playwright.Core.Pages
{
    public class LoginPage : PageBase
    {
        public LoginPage(IPage page) : base(page) { }

        // 🔹 Locators
        private ILocator UsernameInput => Page.GetByPlaceholder("Username");
        private ILocator PasswordInput => Page.GetByPlaceholder("Password");
        private ILocator LoginButton => Page.GetByRole(AriaRole.Button, new() { Name = "Login" });
        private ILocator Version => Page.Locator(".orangehrm-copyright-wrapper p").First;

        // 🔹 Methods

        public async Task GoToLoginPageAsync()
        {
            await Page.GotoAsync(ConfigManager.Settings.Framework.ORANGEHRM_URL);
        }

        public async Task PerformLoginAsync(bool navigate = true)
        {
            if (navigate)
                await GoToLoginPageAsync();

            await UsernameInput.FillAsync(ConfigManager.Settings.Framework.ORANGEHRM_ADMIN_USER);
            await PasswordInput.FillAsync(ConfigManager.Settings.Framework.ORANGEHRM_ADMIN_PASSWORD);

            await LoginButton.ClickAsync(new LocatorClickOptions { Timeout = 15000 });

            var banner = Page.Locator(".oxd-brand-banner");
            await Assertions.Expect(banner).ToBeVisibleAsync(new() { Timeout = 15000 });
        }

        public async Task AssertVersionAsync(string expectedVersion)
        {
            await Assertions.Expect(Version).ToHaveTextAsync(expectedVersion);
        }
    }
}
