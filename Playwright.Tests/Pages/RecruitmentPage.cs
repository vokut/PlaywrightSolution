using Microsoft.Playwright;
using Playwright.Tests.Models;

namespace Playwright.Tests.Pages
{
    public class RecruitmentPage : PageBase
    {
        public RecruitmentPage(IPage page) : base(page) { }

        // Locators
        private ILocator CandidateProfileContainer =>
            Page.Locator(".orangehrm-card-container")
                .Filter(new() { Has = Page.GetByRole(AriaRole.Heading, new() { Name = "Candidate Profile" }) });

        //Methods

        public async Task SaveCandidateAsync()
        {
            await ButtonClickAsync("Save");
            await Assertions.Expect(CandidateProfileContainer)
                            .ToBeVisibleAsync(new() { Timeout = 15000 });
        }

        public async Task AssertSavedCandidateDetailsAsync(Candidate candidate)
        {
            await Assertions.Expect(Input("First Name", false))
                            .ToHaveValueAsync(candidate.FirstName);

            await Assertions.Expect(Input("Middle Name", false))
                            .ToHaveValueAsync(candidate.MiddleName);

            await Assertions.Expect(Input("Last Name", false))
                            .ToHaveValueAsync(candidate.LastName);

            await Assertions.Expect(Input("Email"))
                            .ToHaveValueAsync(candidate.Email);

            await Assertions.Expect(Input("Contact Number"))
                            .ToHaveValueAsync(candidate.PhoneNumber);

            await Assertions.Expect(Input("Keywords"))
                            .ToHaveValueAsync(candidate.Keywords);
        }
    }
}
