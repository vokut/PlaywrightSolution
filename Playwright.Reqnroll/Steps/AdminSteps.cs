using Reqnroll;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Playwright.Tests.Base;
using Playwright.Tests.Pages;

namespace Playwright.Reqnroll.Steps
{
    [Binding]
    public class AdminSteps
    {
        private readonly PageManager _pageManager;
        private string? _jobTitle;
        private string? _locationName;

        public AdminSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [When(@"I open the ""([^""]+)"" -> ""([^""]+)"" section")]
        public async Task WhenIOpenAdminSubsection(string main, string sub)
        {
            await _pageManager.AdminPage.OpenAdminSectionAsync(main, sub);
        }


        [When("I add a new job title")]
        public async Task WhenIAddANewJobTitle()
        {
            var random = new Random();
            _jobTitle = $"Software Engineer{random.Next(10000, 99999)}";

            await _pageManager.AdminPage.ButtonClickAsync("Add");
            await _pageManager.AdminPage.Input("Job Title").FillAsync(_jobTitle);
            await _pageManager.AdminPage.Input("Job Description")
                .FillAsync("Responsible for developing software solutions.");
            await _pageManager.AdminPage.ButtonClickAsync("Save");
        }

        [When("I add a new location")]
        public async Task WhenIAddANewLocation()
        {
            var random = new Random();
            _locationName = $"Location{random.Next(10000, 99999)}";

            await _pageManager.AdminPage.ButtonClickAsync("Add");
            await _pageManager.AdminPage.Input("Name").FillAsync(_locationName);
            await _pageManager.AdminPage.SelectDropdownOptionAsync("Country", "United States");
            await _pageManager.AdminPage.ButtonClickAsync("Save");
        }

        [When("I create a temporary location")]
        public async Task WhenICreateATemporaryLocation()
        {
            var random = new Random();
            _locationName = $"Location{random.Next(10000, 99999)}";

            await _pageManager.AdminPage.ButtonClickAsync("Add");
            await _pageManager.AdminPage.Input("Name").FillAsync(_locationName);
            await _pageManager.AdminPage.SelectDropdownOptionAsync("Country", "United States");
            await _pageManager.AdminPage.ButtonClickAsync("Save");
            await _pageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.SuccessfullySaved);
        }

        [When("I delete that location")]
        public async Task WhenIDeleteThatLocation()
        {
            await _pageManager.AdminPage.Input("Name").FillAsync(_locationName!);
            await _pageManager.AdminPage.ButtonClickAsync("Search");
            await _pageManager.AdminPage.AssertRecordsFoundHeaderAsync(1);

            var cellValue = await _pageManager.AdminPage.GetCellValueAsync(1, "Name");
            Assert.That(cellValue, Is.EqualTo(_locationName));

            await _pageManager.AdminPage.ClickDeleteIconAsync(1);
            await _pageManager.AdminPage.ConfirmationButtonClickAsync("Yes, Delete");
        }
    }
}
