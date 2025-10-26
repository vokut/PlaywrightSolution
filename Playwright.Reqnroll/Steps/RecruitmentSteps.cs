using Allure.NUnit;
using Allure.NUnit.Attributes;
using Playwright.Tests.Base;
using Playwright.Tests.Models;
using Reqnroll;

namespace Playwright.Reqnroll.Steps
{
    [AllureNUnit]
    [AllureFeature("Recruitment")]
    [AllureSuite("Recruitment Management")]
    [Binding]
    public class RecruitmentSteps
    {
        private readonly PageManager _pageManager;
        private Candidate? _candidate1;
        private Candidate? _candidate2;

        public RecruitmentSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [When(@"I open the Recruitment ""([^""]+)"" section")]
        public async Task WhenIOpenRecruitmentSection(string section)
        {
            await _pageManager.RecruitmentPage.OpenSectionAsync(section);
        }

        [When("I add a new candidate")]
        public async Task WhenIAddANewCandidate()
        {
            _candidate1 = new Candidate();

            await _pageManager.RecruitmentPage.ButtonClickAsync("Add");
            await _pageManager.RecruitmentPage.Input("First Name", false).FillAsync(_candidate1.FirstName);
            await _pageManager.RecruitmentPage.Input("Middle Name", false).FillAsync(_candidate1.MiddleName);
            await _pageManager.RecruitmentPage.Input("Last Name", false).FillAsync(_candidate1.LastName);
            await _pageManager.RecruitmentPage.Input("Email").FillAsync(_candidate1.Email);
            await _pageManager.RecruitmentPage.Input("Contact Number").FillAsync(_candidate1.PhoneNumber);
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate1.Keywords);
            await _pageManager.RecruitmentPage.SaveCandidateAsync();
        }

        [When("I create two temporary candidates")]
        public async Task WhenICreateTwoCandidates()
        {
            _candidate1 = new Candidate();
            _candidate2 = new Candidate();

            // Candidate 1
            await _pageManager.RecruitmentPage.OpenSectionAsync("Candidates");
            await _pageManager.RecruitmentPage.ButtonClickAsync("Add");
            await _pageManager.RecruitmentPage.Input("First Name", false).FillAsync(_candidate1.FirstName);
            await _pageManager.RecruitmentPage.Input("Last Name", false).FillAsync(_candidate1.LastName);
            await _pageManager.RecruitmentPage.Input("Email").FillAsync(_candidate1.Email);
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate1.Keywords);
            await _pageManager.RecruitmentPage.SaveCandidateAsync();

            // Candidate 2
            await _pageManager.RecruitmentPage.OpenSectionAsync("Candidates");
            await _pageManager.RecruitmentPage.ButtonClickAsync("Add");
            await _pageManager.RecruitmentPage.Input("First Name", false).FillAsync(_candidate2.FirstName);
            await _pageManager.RecruitmentPage.Input("Last Name", false).FillAsync(_candidate2.LastName);
            await _pageManager.RecruitmentPage.Input("Email").FillAsync(_candidate2.Email);
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate2.Keywords);
            await _pageManager.RecruitmentPage.SaveCandidateAsync();
        }

        [When("I search for candidates by an invalid keyword")]
        public async Task WhenISearchForInvalidKeyword()
        {
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(Guid.NewGuid().ToString());
            await _pageManager.RecruitmentPage.ButtonClickAsync("Search");
        }

        [When("I search for candidates by the first candidate's keyword")]
        public async Task WhenISearchByFirstCandidatesKeyword()
        {
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate1!.Keywords);
            await _pageManager.RecruitmentPage.ButtonClickAsync("Search");
        }

        [When("I create a temporary candidate")]
        public async Task WhenICreateATemporaryCandidate()
        {
            _candidate1 = new Candidate();

            await _pageManager.RecruitmentPage.OpenSectionAsync("Candidates");
            await _pageManager.RecruitmentPage.ButtonClickAsync("Add");
            await _pageManager.RecruitmentPage.Input("First Name", false).FillAsync(_candidate1.FirstName);
            await _pageManager.RecruitmentPage.Input("Last Name", false).FillAsync(_candidate1.LastName);
            await _pageManager.RecruitmentPage.Input("Email").FillAsync(_candidate1.Email);
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate1.Keywords);
            await _pageManager.RecruitmentPage.SaveCandidateAsync();
        }

        [When("I search for that candidate by keyword")]
        public async Task WhenISearchForCandidateByKeyword()
        {
            await _pageManager.RecruitmentPage.Input("Keywords").FillAsync(_candidate1!.Keywords);
            await _pageManager.RecruitmentPage.ButtonClickAsync("Search");
            await _pageManager.RecruitmentPage.AssertRecordsFoundHeaderAsync(1);
        }

        [When("I delete that candidate")]
        public async Task WhenIDeleteThatCandidate()
        {
            await _pageManager.RecruitmentPage.ClickDeleteIconAsync(1);
            await _pageManager.RecruitmentPage.ConfirmationButtonClickAsync("Yes, Delete");
        }

        [Then("I should see exactly one matching record")]
        public async Task ThenIShouldSeeOneRecord()
        {
            await _pageManager.RecruitmentPage.AssertRecordsFoundHeaderAsync(1);

            var cellValue = await _pageManager.RecruitmentPage.GetCellValueAsync(1, "Candidate");
            Assert.That(cellValue.Trim(), Is.EqualTo($"{_candidate1!.FirstName}  {_candidate1.LastName}"));
        }
    }
}
