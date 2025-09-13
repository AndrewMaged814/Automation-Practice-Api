using System.Collections.Generic;
using AutomationPractice_ui_api.Models.API.GetUser;
using AutomationPractice_ui_api.Utils;
using Xunit.Abstractions;

namespace AutomationPractice_ui_api.StepDefinitions.API.GetUser;

[Binding]
public sealed class GetSingleUser(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<SingleUserResponse>(outputHelper, scenarioContext, _endpoint)

{
    private const string _endpoint = "/api/users/{user_id}";


    [Then("I fetch this user")]
    public void ThenIFetchThisUser()
    {
        Get(pathParams: new Dictionary<string, object>
        {
            { "user_id", scenarioContext.GetValue<int>(ScenarioContextKeys.UserId) }
        });
    }

    [Then("User details should include:")]
    public void ThenUserDetailsShouldInclude(Table table)
    {
        var row = table.Rows[0];

        Assert.Equal(row["email"], Response.Data.Email);
    }
}