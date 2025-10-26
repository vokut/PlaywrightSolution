using Microsoft.Playwright;
using Playwright.Tests.Pages;

namespace Playwright.Tests.Pages
{
    public enum MenuItems
    {
        Recruitment,
        Dashboard,
        PIM,
        Leave,
        Time,
        Performance,
        MyInfo,
        Directory,
        Maintenance,
        Admin,
        Buzz,
        Claim
    }

    public class NavigationPage : PageBase
    {
        public NavigationPage(IPage page) : base(page) { }

        //Locator
        private ILocator MenuItem(MenuItems item)
            => Page.GetByRole(AriaRole.Link, new() { Name = item.ToString() });

        //Method
        public async Task ClickMenuItemAsync(MenuItems item)
        {
            await MenuItem(item).ClickAsync(new LocatorClickOptions { Timeout = 10000 });
        }

        public async Task Wait()
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = MenuItems.Admin.ToString() }).ClearAsync();
        }
    }
}
