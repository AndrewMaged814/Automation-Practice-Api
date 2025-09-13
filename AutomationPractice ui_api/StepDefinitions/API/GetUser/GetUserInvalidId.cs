using System.Collections.Generic;
using AutomationPractice_ui_api.StepDefinitions.API;
using Xunit.Abstractions;

namespace AutomationPractice_ui_api.StepDefinitions.API.GetUser;

[Binding]
internal sealed class GetUserInvalidId(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<object>(outputHelper, scenarioContext, _endpoint)

{
    private const string _endpoint = "/api/users/{user_id}";


    [When("I request get user using invalid id")]
    public void WhenIRequestGetUserUsingInvalidId()
    {
        Get(pathParams: new Dictionary<string, object>
        {
            { "user_id", 23 }
        });
    }
}