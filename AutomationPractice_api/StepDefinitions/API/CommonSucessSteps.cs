using System.Net;
using System.Threading.Tasks;
using AutomationPractice_api.Utils;
using AutomationPractice_ui_api.Utils;

namespace AutomationPractice_api.StepDefinitions.API;

[Binding]
public class CommonSuccessTest(ScenarioContext scenarioContext)
{
    [Then("User receives empty success response")]
    public void ThenUserReceivesEmptySuccessResponse()
    {
        var responseBody = scenarioContext.Get<object>(ScenarioContextKeys.ResponseKey);
        var statusCode = scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

        AssertHelper.AssertSuccessEmptyResponse(statusCode, responseBody);
    }

    [Then("User receives success response")]
    public void ThenUserReceivesSuccessResponse()
    {
        var responseBody = scenarioContext.Get<object>(ScenarioContextKeys.ResponseKey);
        var statusCode = scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

        AssertHelper.AssertSuccessResponse(statusCode, responseBody);
    }

    [Then(@"User wait for (.*) seconds")]
    public async Task ThenUserWaitForSecondsAsync(int seconds)
    {
        await Task.Delay(seconds * 1000);
    }
}