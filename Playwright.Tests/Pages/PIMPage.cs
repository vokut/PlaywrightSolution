using Microsoft.Playwright;
using Playwright.Tests.Models;

namespace Playwright.Tests.Pages
{
    public class PIMPage : PageBase
    {
        public PIMPage(IPage page) : base(page) { }

        // 🔹 Locator helper
        private ILocator EmployeeProfileContainer(string sectionName)
        {
            return Page.Locator(".orangehrm-card-container")
                       .Filter(new() { Has = Page.GetByRole(AriaRole.Heading, new() { Name = sectionName }) });
        }

        // 🔹 Methods

        public async Task SaveEmployeeAsync()
        {
            await ButtonClickAsync("Save");
            await AssertPopupMessageAsync(PopupMessages.SuccessfullySaved);

            var personalDetails = EmployeeProfileContainer("Personal Details");
            await Assertions.Expect(personalDetails)
                            .ToBeVisibleAsync(new() { Timeout = 10000 });
        }

        public async Task AssertSavedEmployeeDetailsAsync(Employee employee)
        {
            await Assertions.Expect(Input("First name", false))
                            .ToHaveValueAsync(employee.FirstName);

            await Assertions.Expect(Input("Middle Name", false))
                            .ToHaveValueAsync(employee.MiddleName);

            await Assertions.Expect(Input("Last Name", false))
                            .ToHaveValueAsync(employee.LastName);

            await Assertions.Expect(Input("Employee Id"))
                            .ToHaveValueAsync(employee.EmployeeId);
        }

        public async Task OpenEmployeeSectionAsync(string sectionName)
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = sectionName })
                      .ClickAsync();
        }
    }
}
