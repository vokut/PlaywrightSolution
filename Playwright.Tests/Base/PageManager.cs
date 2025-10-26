using Microsoft.Playwright;
using Playwright.Tests.Pages;

namespace Playwright.Tests.Base
{
    public class PageManager
    {
        private readonly LoginPage _loginPage;
        private readonly NavigationPage _navigationPage;
        private readonly RecruitmentPage _recruitmentPage;
        private readonly AdminPage _adminPage;
        private readonly PIMPage _pimPage;

        public PageManager(IPage page)
        {
            _loginPage = new LoginPage(page);
            _navigationPage = new NavigationPage(page);
            _recruitmentPage = new RecruitmentPage(page);
            _adminPage = new AdminPage(page);
            _pimPage = new PIMPage(page);
        }

        public LoginPage LoginPage => _loginPage;
        public NavigationPage NavigationPage => _navigationPage;
        public RecruitmentPage RecruitmentPage => _recruitmentPage;
        public AdminPage AdminPage => _adminPage;
        public PIMPage PIMPage => _pimPage;
    }
}
