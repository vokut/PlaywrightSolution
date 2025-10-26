using Allure.NUnit;
using Allure.NUnit.Attributes;
using Playwright.Tests.Pages;
using Playwright.Tests.Base;
using Playwright.Tests.Models;

namespace Playwright.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [AllureNUnit]
    [AllureFeature("Recruitment")]
    [AllureSuite("Recruitment Management")]
    public class RecruitmentTests : TestBase
    {
        [Test]
        public async Task CreateCandidate()
        {
            var candidate = new Candidate();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.OpenSectionAsync("Candidates");

            await PageManager.RecruitmentPage.ButtonClickAsync("Add");
            await PageManager.RecruitmentPage.Input("First Name", false).FillAsync(candidate.FirstName);
            await PageManager.RecruitmentPage.Input("Middle Name", false).FillAsync(candidate.MiddleName);
            await PageManager.RecruitmentPage.Input("Last Name", false).FillAsync(candidate.LastName);
            await PageManager.RecruitmentPage.Input("Email").FillAsync(candidate.Email);
            await PageManager.RecruitmentPage.Input("Contact Number").FillAsync(candidate.PhoneNumber);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate.Keywords);
            await PageManager.RecruitmentPage.SaveCandidateAsync();

            await PageManager.RecruitmentPage.AssertSavedCandidateDetailsAsync(candidate);
        }

        [Test]
        public async Task SearchCandidateByKeywords()
        {
            var candidate1 = new Candidate();
            var candidate2 = new Candidate();

            await PageManager.LoginPage.PerformLoginAsync();

            // Create first candidate
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.OpenSectionAsync("Candidates");
            await PageManager.RecruitmentPage.ButtonClickAsync("Add");
            await PageManager.RecruitmentPage.Input("First Name", false).FillAsync(candidate1.FirstName);
            await PageManager.RecruitmentPage.Input("Last Name", false).FillAsync(candidate1.LastName);
            await PageManager.RecruitmentPage.Input("Email").FillAsync(candidate1.Email);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate1.Keywords);
            await PageManager.RecruitmentPage.SaveCandidateAsync();

            // Create second candidate
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.OpenSectionAsync("Candidates");
            await PageManager.RecruitmentPage.ButtonClickAsync("Add");
            await PageManager.RecruitmentPage.Input("First Name", false).FillAsync(candidate2.FirstName);
            await PageManager.RecruitmentPage.Input("Last Name", false).FillAsync(candidate2.LastName);
            await PageManager.RecruitmentPage.Input("Email").FillAsync(candidate2.Email);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate2.Keywords);
            await PageManager.RecruitmentPage.SaveCandidateAsync();

            // Search with fake keywords
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(Guid.NewGuid().ToString());
            await PageManager.RecruitmentPage.ButtonClickAsync("Search");
            await PageManager.RecruitmentPage.AssertPopupMessageAsync(PopupMessages.NoRecordsFound);

            // Search with correct keywords
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate1.Keywords);
            await PageManager.RecruitmentPage.ButtonClickAsync("Search");
            await PageManager.RecruitmentPage.AssertRecordsFoundHeaderAsync(1);

            var cellValue = await PageManager.RecruitmentPage.GetCellValueAsync(1, "Candidate");
            Assert.That(cellValue.Trim(), Is.EqualTo($"{candidate1.FirstName}  {candidate1.LastName}"));
        }

        [Test]
        public async Task DeleteCandidate()
        {
            var candidate = new Candidate();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.OpenSectionAsync("Candidates");

            await PageManager.RecruitmentPage.ButtonClickAsync("Add");
            await PageManager.RecruitmentPage.Input("First Name", false).FillAsync(candidate.FirstName);
            await PageManager.RecruitmentPage.Input("Last Name", false).FillAsync(candidate.LastName);
            await PageManager.RecruitmentPage.Input("Email").FillAsync(candidate.Email);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate.Keywords);
            await PageManager.RecruitmentPage.SaveCandidateAsync();

            // Search for and delete the candidate
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Recruitment);
            await PageManager.RecruitmentPage.Input("Keywords").FillAsync(candidate.Keywords);
            await PageManager.RecruitmentPage.ButtonClickAsync("Search");
            await PageManager.RecruitmentPage.AssertRecordsFoundHeaderAsync(1);

            var candidateName = await PageManager.RecruitmentPage.GetCellValueAsync(1, "Candidate");
            Assert.That(candidateName.Trim(), Is.EqualTo($"{candidate.FirstName}  {candidate.LastName}"));

            await PageManager.RecruitmentPage.ClickDeleteIconAsync(1);
            await PageManager.RecruitmentPage.ConfirmationButtonClickAsync("Yes, Delete");
            await PageManager.RecruitmentPage.AssertPopupMessageAsync(PopupMessages.SuccessfullyDeleted);
            await PageManager.RecruitmentPage.AssertPopupMessageAsync(PopupMessages.NoRecordsFound);
        }
    }
}
