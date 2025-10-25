using Allure.Net.Commons;
using dotenv.net;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Playwright.Core.Models;

namespace Playwright.Core.Config
{
    public static class ConfigManager
    {
        private static IConfigurationRoot? _config;
        public static TestSettings Settings { get; private set; } = new();
        public static string EnvironmentName { get; private set; } = "dev";

        public static void Initialize()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();

            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && !File.Exists(Path.Combine(dir.FullName, ".env")) && !dir.GetFiles("*.sln").Any())
            {
                dir = dir.Parent;
            }

            if (dir == null)
            {
                throw new DirectoryNotFoundException("Could not locate solution directory or .env file.");
            }

            var envPath = Path.Combine(dir.FullName, ".env");

            if (File.Exists(envPath))
            {
                DotEnv.Load(new DotEnvOptions(envFilePaths: [envPath], overwriteExistingVars: false));

            }
            //var envFile = Path.Combine(TestContext.CurrentContext.TestDirectory, ".env");
            //if (File.Exists(envFile))
            //    Env.Load(envFile);

            EnvironmentName = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "dev";

            var builder = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            _config = builder.Build();

            Settings = new TestSettings();
            _config.Bind(Settings);
            TestContext.WriteLine($"Loaded config for {EnvironmentName}");
        }
    }
}