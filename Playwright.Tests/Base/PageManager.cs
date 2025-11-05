using Microsoft.Playwright;
using Playwright.Tests.Pages;

namespace Playwright.Tests.Base
{
    public class PageManager(IPage page)
    {
        private readonly LoginPage _loginPage = new LoginPage(page);
        private readonly NavigationPage _navigationPage = new NavigationPage(page);
        private readonly RecruitmentPage _recruitmentPage = new RecruitmentPage(page);
        private readonly AdminPage _adminPage = new AdminPage(page);
        private readonly PIMPage _pimPage = new PIMPage(page);

        public LoginPage LoginPage => _loginPage;
        public NavigationPage NavigationPage => _navigationPage;
        public RecruitmentPage RecruitmentPage => _recruitmentPage;
        public AdminPage AdminPage => _adminPage;
        public PIMPage PIMPage => _pimPage;
    }
}
