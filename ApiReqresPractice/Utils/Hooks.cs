using Common.Utils;
using ApiReqresPractice.Utils;
using Xunit.Abstractions;

namespace ApiReqresPractice.Utils;

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