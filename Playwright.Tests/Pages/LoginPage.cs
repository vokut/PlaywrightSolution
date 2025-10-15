using Microsoft.Playwright;
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
            // You can replace this with your ConfigManager if you store URLs in JSON files
            var orangeHrmUrl = Environment.GetEnvironmentVariable("ORANGEHRM_URL");
            if (string.IsNullOrEmpty(orangeHrmUrl))
                throw new InvalidOperationException("Environment variable 'ORANGEHRM_URL' is not set.");

            await Page.GotoAsync(orangeHrmUrl);
        }

        public async Task PerformLoginAsync(bool navigate = true)
        {
            if (navigate)
                await GoToLoginPageAsync();

            var username = Environment.GetEnvironmentVariable("ORANGEHRM_USER");
            var password = Environment.GetEnvironmentVariable("ORANGEHRM_PASSWORD");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Environment variables ORANGEHRM_USER and ORANGEHRM_PASSWORD must be set.");

            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);

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
