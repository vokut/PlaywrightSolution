using Playwright.Tests.Base;
using Playwright.Tests.Pages; // for MenuItems enum
using Reqnroll;

namespace Playwright.Reqnroll.Steps
{
    [Binding]
    public class NavigationSteps
    {
        private readonly PageManager _pageManager;

        public NavigationSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [When(@"I navigate to the ""(.*)"" section")]
        public async Task WhenINavigateToTheSection(string section)
        {
            if (!Enum.TryParse<MenuItems>(section, true, out var menu))
                throw new ArgumentException($"Invalid menu name: {section}");

            await _pageManager.NavigationPage.ClickMenuItemAsync(menu);
        }
    }
}
