namespace Playwright.Core.Models
{
    public class TestSettings
    {
        public FrameworkSettings Framework { get; set; } = new();
        public PlaywrightSettings Playwright { get; set; } = new();
    }
    public class FrameworkSettings
    {
        public string ORANGEHRM_URL { get; set; } = "";
        public string ORANGEHRM_ADMIN_USER { get; set; } = "";
        public string ORANGEHRM_ADMIN_PASSWORD { get; set; } = "";
        public string ArtifactsDir { get; set; } = "Artifacts";

    }
    public class PlaywrightSettings
    {
        public string Browser { get; set; } = "chromium";
        public bool Headless { get; set; } = true;
        public bool RecordTrace { get; set; } = true;
        public bool RecordVideo { get; set; } = false;
        public string VideoDir { get; set; } = "Artifacts/Videos";
        public string TraceDir { get; set; } = "Artifacts/Traces";
        public ViewportSettings Viewport { get; set; } = new();
        public int DefaultTimeout { get; set; } = 15000;
        public int NavigationTimeout { get; set; } = 30000;

    }
    public class ViewportSettings
    {
        public int Width { get; set; } = 1600;
        public int Height { get; set; } = 900;
        public double DeviceScaleFactor { get; set; } = 1.0;
    }
}