namespace Playwright.Core.Models
{
    public class ConfigModel
    {
        public string SearchWord { get; set; } = string.Empty;
        public string Browser { get; set; } = string.Empty;
        public bool Headless { get; set; }
        public string BaseUrl { get; set; } = string.Empty;
        public int BrowserWidth { get; set; } = 1920;
        public int BrowserHeight { get; set; } = 1080;

        // New timeout settings (milliseconds)
        public int Timeout { get; set; } = 30000; 
        public int NavigationTimeout { get; set; } = 30000;
        public int ActionTimeout { get; set; } = 10000;
    }
}
