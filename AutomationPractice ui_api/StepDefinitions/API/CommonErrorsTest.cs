using System.Net;
using AutomationPractice_ui_api.Models;
using AutomationPractice_ui_api.Utils;

namespace AutomationPractice_ui_api.StepDefinitions.API;

[Binding]
public class CommonErrorsTest(ScenarioContext scenarioContext)
{
    [Then("User receives an error")]
    public void ThenUserReceivesAnError(Table errorTable)
    {
        var statusCode = scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
        var response = scenarioContext.Get<ErrorResponse>(ScenarioContextKeys.ResponseKey);

        Assert.Equal(HttpStatusCode.BadRequest, statusCode);

        var expectedError = errorTable.Rows[0]["error"]; 
        Assert.Equal(expectedError, response.Error);
    }
    [Then("User receives not found response")]
    public void ThenUserReceivesNotFoundResponse()
    {
        var statusCode = scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

}