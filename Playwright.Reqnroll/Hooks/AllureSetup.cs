using Allure.Net.Commons;
using Allure.NUnit;

[AllureNUnit]
[SetUpFixture]
public class AllureSetup
{
    [OneTimeSetUp]
    public void InitAllure()
    {
        // Make sure the Allure lifecycle and listener are active for this assembly
        _ = AllureLifecycle.Instance;
    }
}
