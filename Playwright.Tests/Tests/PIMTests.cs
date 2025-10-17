using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Playwright.Core.Models;
using Playwright.Core.Pages;
using Playwright.Test.Pages;
using Playwright.Tests.Base;

namespace Playwright.Tests
{
    [TestFixture, Ignore("TEMP")]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    [AllureNUnit]
    [AllureFeature("PIM")]
    [AllureSuite("PIM Management")]
    public class EmployeeTests : TestBase
    {
        [Test]
        public async Task CreateEmployee()
        {
            var employee = new Employee();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.RecruitmentPage.OpenSectionAsync("Add Employee");

            await PageManager.PIMPage.Input("First Name", false).FillAsync(employee.FirstName);
            await PageManager.PIMPage.Input("Middle Name", false).FillAsync(employee.MiddleName);
            await PageManager.PIMPage.Input("Last Name", false).FillAsync(employee.LastName);
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.SaveEmployeeAsync();

            await PageManager.PIMPage.AssertSavedEmployeeDetailsAsync(employee);
        }

        [Test]
        public async Task EditEmployeeContactDetails()
        {
            var employee = new Employee();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Add Employee");

            await PageManager.PIMPage.Input("First Name", false).FillAsync(employee.FirstName);
            await PageManager.PIMPage.Input("Middle Name", false).FillAsync(employee.MiddleName);
            await PageManager.PIMPage.Input("Last Name", false).FillAsync(employee.LastName);
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.SaveEmployeeAsync();

            // Navigate to Employee List
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Employee List");

            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.ButtonClickAsync("Search");
            await PageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);

            await PageManager.PIMPage.ClickPencilIconAsync(1);
            await PageManager.PIMPage.OpenEmployeeSectionAsync("Contact Details");

            var newEmail = $"test_{Guid.NewGuid():N}@example.com";
            await PageManager.PIMPage.Input("Work Email").FillAsync(newEmail);
            await PageManager.PIMPage.ButtonClickAsync("Save");
        }

        [Test]
        public async Task DeleteEmployee()
        {
            var employee = new Employee();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Add Employee");

            await PageManager.PIMPage.Input("First Name", false).FillAsync(employee.FirstName);
            await PageManager.PIMPage.Input("Last Name", false).FillAsync(employee.LastName);
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.SaveEmployeeAsync();

            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Employee List");
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.ButtonClickAsync("Search");
            await PageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);

            await PageManager.PIMPage.ClickDeleteIconAsync(1);
            await PageManager.PIMPage.ConfirmationButtonClickAsync("Yes, Delete");
            await PageManager.PIMPage.AssertPopupMessageAsync(PopupMessages.SuccessfullyDeleted);
            await PageManager.PIMPage.AssertPopupMessageAsync(PopupMessages.NoRecordsFound);
        }

        [Test]
        public async Task CancelEmployeeDeletion()
        {
            var employee = new Employee();

            await PageManager.LoginPage.PerformLoginAsync();
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Add Employee");

            await PageManager.PIMPage.Input("First Name", false).FillAsync(employee.FirstName);
            await PageManager.PIMPage.Input("Last Name", false).FillAsync(employee.LastName);
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.SaveEmployeeAsync();

            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Employee List");
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.ButtonClickAsync("Search");
            await PageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);

            await PageManager.PIMPage.ClickDeleteIconAsync(1);
            await PageManager.PIMPage.ConfirmationButtonClickAsync("No, Cancel");

            // Verify still visible after cancel
            await PageManager.NavigationPage.ClickMenuItemAsync(MenuItems.PIM);
            await PageManager.PIMPage.OpenSectionAsync("Employee List");
            await PageManager.PIMPage.Input("Employee Id").FillAsync(employee.EmployeeId);
            await PageManager.PIMPage.ButtonClickAsync("Search");
            await PageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);
        }
    }
}
