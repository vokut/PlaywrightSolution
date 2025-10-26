using Playwright.Tests.Base;
using Playwright.Tests.Pages;
using Reqnroll;

namespace Playwright.Reqnroll.Steps
{
    [Binding]
    public class MessagesSteps
    {
        private readonly PageManager _pageManager;

        public MessagesSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [Then(@"I should see the ""([^""]+)"" message")]
        public async Task ThenIShouldSeeTheMessage(string messageText)
        {
            // Try match enum ignoring spaces
            var normalized = messageText.Replace(" ", string.Empty);

            if (!Enum.TryParse<PopupMessages>(normalized, true, out var popupType))
                Assert.Fail($"Unknown popup message type: '{messageText}' (normalized: '{normalized}')");

            await _pageManager.PIMPage.AssertPopupMessageAsync(popupType);
        }

    }
}
