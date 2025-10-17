using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Playwright.Core.Pages;
using Playwright.Test.Pages;
using Playwright.Tests.Base;

namespace Playwright.Tests
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    [AllureNUnit]
    [AllureFeature("Admin")]
    [AllureSuite("Admin Management")]
    public class AdminTests : TestBase
    {
        [Test]
        public async Task CreateJobTitle()
        {
            Assert.Fail("FF");
            var random = new Random();
            var jobTitle = $"Software Engineer{random.Next(10000, 99999)}";

            // Login and navigate to Admin > Job > Job Titles
            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Admin);
            await PageManager.AdminPage.OpenAdminSectionAsync("Job", "Job Titles");

            // Add new job title
            await PageManager.AdminPage.ButtonClickAsync("Add");
            await PageManager.AdminPage.Input("Job Title").FillAsync(jobTitle);
            await PageManager.AdminPage.Input("Job Description")
                               .FillAsync("Responsible for developing software solutions.");
            await PageManager.AdminPage.ButtonClickAsync("Save");
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.SuccessfullySaved);

            // Navigate to PIM > verify job title exists in dropdown
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.AdminPage.SelectDropdownOptionAsync("Job Title", jobTitle);
        }

        [Test]
        public async Task CreateLocation()
        {
            var random = new Random();
            var locationName = $"Location{random.Next(10000, 99999)}";

            // Login and navigate to Admin > Organization > Locations
            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Admin);
            await PageManager.AdminPage.OpenAdminSectionAsync("Organization", "Locations");

            // Add a new location
            await PageManager.AdminPage.ButtonClickAsync("Add");
            await PageManager.AdminPage.Input("Name").FillAsync(locationName);
            await PageManager.AdminPage.SelectDropdownOptionAsync("Country", "United States");
            await PageManager.AdminPage.ButtonClickAsync("Save");
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.SuccessfullySaved);
        }

        [Test]
        public async Task DeleteLocation()
        {
            var random = new Random();
            var locationName = $"Location{random.Next(10000, 99999)}";

            // Login and navigate to Admin > Organization > Locations
            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.Admin);
            await PageManager.AdminPage.OpenAdminSectionAsync("Organization", "Locations");

            // Add a new location
            await PageManager.AdminPage.ButtonClickAsync("Add");
            await PageManager.AdminPage.Input("Name").FillAsync(locationName);
            await PageManager.AdminPage.SelectDropdownOptionAsync("Country", "United States");
            await PageManager.AdminPage.ButtonClickAsync("Save");
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.SuccessfullySaved);

            // Search for the location and delete it
            await PageManager.AdminPage.Input("Name").FillAsync(locationName);
            await PageManager.AdminPage.ButtonClickAsync("Search");
            await PageManager.AdminPage.AssertRecordsFoundHeaderAsync(1);

            var cellValue = await PageManager.AdminPage.GetCellValueAsync(1, "Name");
            Assert.That(cellValue, Is.EqualTo(locationName));

            await PageManager.AdminPage.ClickDeleteIconAsync(1);
            await PageManager.AdminPage.ConfirmationButtonClickAsync("Yes, Delete");
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.SuccessfullyDeleted);
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.NoRecordsFound);
            await PageManager.AdminPage.ButtonClickAsync("Search");
            await PageManager.AdminPage.AssertPopupMessageAsync(PopupMessages.NoRecordsFound);
        }
    }
}
