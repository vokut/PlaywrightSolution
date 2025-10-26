using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Playwright.Tests.Pages
{
    public class AdminPage : PageBase
    {
        public AdminPage(IPage page) : base(page) { }

        // Methods

        public async Task OpenAdminSectionAsync(string mainSection, string subSection)
        {
            var navigationPage = new NavigationPage(Page);

            // Navigate to the Admin menu
            await navigationPage.ClickMenuItemAsync(MenuItems.Admin);

            // Click main section (e.g. “User Management”)
            var topBarItem = Page
                .Locator(".oxd-topbar-body")
                .Locator("li")
                .Filter(new() { HasText = mainSection });

            await topBarItem.ClickAsync();

            // Click sub-section (e.g. “Users”)
            var subSectionLocator = Page
                .GetByRole(AriaRole.Listitem)
                .Filter(new() { HasTextRegex = new Regex($"^{Regex.Escape(subSection)}$") });

            await subSectionLocator.ClickAsync();
        }
    }
}
