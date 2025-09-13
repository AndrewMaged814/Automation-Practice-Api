using System.Collections.Generic;
using AutomationPractice_api.Models.API;
using Xunit.Abstractions;

namespace AutomationPractice_api.StepDefinitions.API.Register;

[Binding]
public sealed class RegisterNegativeCases(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<ErrorResponse>(outputHelper, scenarioContext, "/api/register")
{
    [When("I send attempt registration request with the following data:")]
    public void WhenISendAttemptRegistrationRequestWithTheFollowingData(Table table)
    {
        var row = table.Rows[0];

        var email = row.ContainsKey("email") ? row["email"] : null;

        var payload = new Dictionary<string, object>();
        if (!string.IsNullOrWhiteSpace(email)) payload["email"] = email;

        Post(payload); 
    }
}