using Playwright.Core.Models;

namespace Playwright.Core.Utilities
{
    public static class ConfigManager
    {
        private static ConfigModel? _config;

        public static void Initialize(ConfigModel config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public static ConfigModel Config =>
            _config ?? throw new InvalidOperationException(
                "ConfigManager not initialized. GlobalTestSetup must run before accessing Config."
            );

    }
}
