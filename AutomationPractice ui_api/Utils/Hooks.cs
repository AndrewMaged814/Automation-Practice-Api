using Common.Utils;
using AutomationPractice_ui_api.Utils;
using Xunit.Abstractions;

namespace AutomationPractice_ui_api.Utils;

[Binding]
public sealed class Hooks(ScenarioContext scenarioContext, ITestOutputHelper outputHelper)
{
    
    [BeforeTestRun]
    public static void BeforeAll()
    {
        var configurations = ConfigurationsHelper.GetConfigurations();
        Constants.BASE_URL = configurations!.BaseUrl;
        
    }


}