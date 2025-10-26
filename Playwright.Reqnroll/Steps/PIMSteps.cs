using Reqnroll;
using Playwright.Tests.Models;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Playwright.Tests.Base;
using Playwright.Tests.Pages;

namespace Playwright.Reqnroll.Steps
{
    [Binding]
    public class PIMSteps
    {
        private readonly PageManager _pageManager;
        private Employee? _employee;

        public PIMSteps(ScenarioContext scenarioContext)
        {
            _pageManager = (scenarioContext["PageManager"] as PageManager)!;
        }

        [When(@"I open the PIM ""([^""]+)"" section")]
        public async Task WhenIOpenPIMSection(string section)
        {
            await _pageManager.PIMPage.OpenSectionAsync(section);
        }

        [When("I add a new employee")]
        public async Task WhenIAddANewEmployee()
        {
            _employee = new Employee();

            await _pageManager.PIMPage.Input("First Name", false).FillAsync(_employee.FirstName);
            await _pageManager.PIMPage.Input("Middle Name", false).FillAsync(_employee.MiddleName);
            await _pageManager.PIMPage.Input("Last Name", false).FillAsync(_employee.LastName);
            await _pageManager.PIMPage.Input("Employee Id").FillAsync(_employee.EmployeeId);
            await _pageManager.PIMPage.SaveEmployeeAsync();
        }

        [When("I create a temporary employee")]
        public async Task WhenICreateATemporaryEmployee()
        {
            _employee = new Employee();

            await _pageManager.PIMPage.OpenSectionAsync("Add Employee");
            await _pageManager.PIMPage.Input("First Name", false).FillAsync(_employee.FirstName);
            await _pageManager.PIMPage.Input("Last Name", false).FillAsync(_employee.LastName);
            await _pageManager.PIMPage.Input("Employee Id").FillAsync(_employee.EmployeeId);
            await _pageManager.PIMPage.SaveEmployeeAsync();
        }

        [When("I search for that employee by ID")]
        public async Task WhenISearchForThatEmployeeById()
        {
            await _pageManager.PIMPage.Input("Employee Id").FillAsync(_employee!.EmployeeId);
            await _pageManager.PIMPage.ButtonClickAsync("Search");
            await _pageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);
        }

        [When("I edit the employee's contact details")]
        public async Task WhenIEditTheEmployeesContactDetails()
        {
            await _pageManager.PIMPage.ClickPencilIconAsync(1);
            await _pageManager.PIMPage.OpenEmployeeSectionAsync("Contact Details");

            var newEmail = $"test_{Guid.NewGuid():N}@example.com";
            await _pageManager.PIMPage.Input("Work Email").FillAsync(newEmail);
            await _pageManager.PIMPage.ButtonClickAsync("Save");
        }

        [When("I delete that employee")]
        public async Task WhenIDeleteThatEmployee()
        {
            await _pageManager.PIMPage.ClickDeleteIconAsync(1);
            await _pageManager.PIMPage.ConfirmationButtonClickAsync("Yes, Delete");
        }

        [When("I start deleting the employee but cancel the confirmation")]
        public async Task WhenICancelEmployeeDeletion()
        {
            await _pageManager.PIMPage.ClickDeleteIconAsync(1);
            await _pageManager.PIMPage.ConfirmationButtonClickAsync("No, Cancel");
        }

        [Then("I should still find that employee in the list")]
        public async Task ThenIShouldStillFindEmployee()
        {
            await _pageManager.PIMPage.AssertRecordsFoundHeaderAsync(1);
        }
    }
}
