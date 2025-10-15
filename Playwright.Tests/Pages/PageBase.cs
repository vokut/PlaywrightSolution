using Microsoft.Playwright;

namespace Playwright.Test.Pages
{
    public enum PopupMessages
    {
        NoRecordsFound,
        SuccessfullyDeleted,
        SuccessfullySaved
    }

    public class PageBase
    {
        public readonly IPage Page;

        public PageBase(IPage page)
        {
            Page = page;
        }

        /// <summary>
        /// Wait for a specific number of seconds
        /// </summary>
        public async Task WaitAsync(int seconds)
        {
            await Page.WaitForTimeoutAsync(seconds * 1000);
        }

        public async Task SelectDropdownOptionAsync(string label, string option)
        {
            var field = Page.Locator(".oxd-input-field-bottom-space")
                .Filter(new() { HasText = label })
                .Locator(".oxd-select-text-input");

            await field.ClickAsync();

            var optionLocator = Page.GetByRole(AriaRole.Option, new() { Name = option, Exact = true });
            await optionLocator.ScrollIntoViewIfNeededAsync();
            await optionLocator.ClickAsync();
        }

        public ILocator Input(string name, bool hasLabel = true)
        {
            if (!hasLabel)
                return Page.GetByPlaceholder(name);

            return Page.Locator(".oxd-input-field-bottom-space")
                .Filter(new() { HasText = name })
                .GetByRole(AriaRole.Textbox);
        }

        public async Task AssertPopupMessageAsync(PopupMessages popupMessage)
        {
            string messageText = popupMessage switch
            {
                PopupMessages.NoRecordsFound => "No Records Found",
                PopupMessages.SuccessfullyDeleted => "Successfully Deleted",
                PopupMessages.SuccessfullySaved => "Successfully Saved",
                _ => throw new ArgumentOutOfRangeException(nameof(popupMessage), popupMessage, null)
            };

            var messageLocator = Page.Locator("p", new() { HasTextString = messageText });
            await Assertions.Expect(messageLocator).ToBeVisibleAsync();
            await Assertions.Expect(messageLocator).Not.ToBeVisibleAsync();
        }

        public async Task AssertRecordsFoundHeaderAsync(int numberOfRecords)
        {
            var recordsText = $"({numberOfRecords}) Record{(numberOfRecords == 1 ? "" : "s")} Found";
            var locator = Page.Locator(".oxd-text", new() { HasTextString = recordsText });
            await Assertions.Expect(locator).ToBeVisibleAsync();
        }

        public async Task<string> GetCellValueAsync(int row, string columnName)
        {
            var table = Page.Locator(".oxd-table");
            var headers = await table.Locator(".oxd-table-header").Locator(".oxd-table-header-cell").AllAsync();

            int headerIndex = -1;

            for (int i = 0; i < headers.Count; i++)
            {
                var headerText = await headers[i].EvaluateAsync<string>(@"el => {
                    for (const node of el.childNodes) {
                        if (node.nodeType === Node.TEXT_NODE && node.textContent?.trim()) {
                            return node.textContent.trim();
                        }
                    }
                    return '';
                }");

                if (headerText.Trim() == columnName)
                {
                    headerIndex = i;
                    break;
                }
            }

            if (headerIndex == -1)
                throw new Exception($"Column '{columnName}' not found.");

            var cellLocator = table
                .Locator(".oxd-table-card").Nth(row - 1)
                .Locator(".oxd-table-cell").Nth(headerIndex);

            var cellValue = await cellLocator.TextContentAsync();
            return cellValue?.Trim() ?? string.Empty;
        }

        public async Task ClickPencilIconAsync(int row)
        {
            var rows = Page.Locator(".oxd-table .oxd-table-body .oxd-table-row");
            await rows.Nth(row - 1).Locator(".bi-pencil-fill").ClickAsync();
        }

        public async Task ClickDeleteIconAsync(int row)
        {
            var rows = Page.Locator(".oxd-table .oxd-table-body .oxd-table-row");
            await rows.Nth(row - 1).Locator(".bi-trash").ClickAsync();
        }

        public async Task OpenSectionAsync(string sectionName)
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = sectionName }).ClickAsync();
        }

        public async Task ButtonClickAsync(string buttonName)
        {
            var button = Page.GetByRole(AriaRole.Button, new() { Name = buttonName });
            await button.ClickAsync(new LocatorClickOptions { Timeout = 15000 });
        }

        public async Task ConfirmationButtonClickAsync(string buttonName)
        {
            var confirmButton = Page.GetByRole(AriaRole.Button, new() { Name = buttonName });
            await Assertions.Expect(confirmButton).ToBeVisibleAsync();
            await confirmButton.ClickAsync();
        }
    }
}
