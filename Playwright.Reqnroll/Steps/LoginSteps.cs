using Reqnroll;
using Playwright.Tests.Pages;
using System.Threading.Tasks;
using Playwright.Tests.Base;

namespace Playwright.Reqnroll.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly PageManager _pageManager;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [Given("I am logged in as admin")]
        public async Task GivenIAmLoggedInAsAdmin()
        {
            await _pageManager.LoginPage.PerformLoginAsync();
        }
    }
}
