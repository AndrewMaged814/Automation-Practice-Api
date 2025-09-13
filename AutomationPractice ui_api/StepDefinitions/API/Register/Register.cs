using System.Collections.Generic;
using AutomationPractice_ui_api.Models.API.Register;
using AutomationPractice_ui_api.StepDefinitions.API;
using AutomationPractice_ui_api.Utils;
using Xunit.Abstractions;

[Binding]
public sealed class Register(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<RegisterResponse>(outputHelper, scenarioContext, _endpoint)
{
    private const string _endpoint = "/api/register";

    [When("I send a registration request with the following data:")]
    public void WhenISendARegistrationRequestWithTheFollowingData(Table table)
    {
        var row = table.Rows[0];

        var email = row.ContainsKey("email") ? row["email"] : null;
        var password = row.ContainsKey("password") ? row["password"] : null;

        var payload = new Dictionary<string, object>();
        if (!string.IsNullOrWhiteSpace(email)) payload["email"] = email;
        if (!string.IsNullOrWhiteSpace(password)) payload["password"] = password;

        Post(payload); 
        
        if (Response != null && Response.Id != 0) scenarioContext.SetValue(ScenarioContextKeys.UserId, Response.Id);
    }
}