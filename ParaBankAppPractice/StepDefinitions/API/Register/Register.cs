using System.Collections.Generic;
using ParaBankAppPractice.Models;
using ParaBankAppPractice.Models.API.Register;
using ParaBankAppPractice.Utils;
using Xunit.Abstractions;

namespace ParaBankAppPractice.StepDefinitions.API.Register;

[Binding]
public sealed class Register(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<object>(outputHelper, scenarioContext, _endpoint)
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

        PostRaw<Dictionary<string, object>, RegisterResponse>(payload);

        var response = scenarioContext.Get<RegisterResponse>(ScenarioContextKeys.ResponseKey);

        if (response != null && response.Id != 0) scenarioContext.SetValue(ScenarioContextKeys.UserId, response.Id);
    }

    [When("I send attempt registration request with the following data:")]
    public void WhenISendAttemptRegistrationRequestWithTheFollowingData(Table table)
    {
        var row = table.Rows[0];

        var email = row.ContainsKey("email") ? row["email"] : null;

        var payload = new Dictionary<string, object>();
        if (!string.IsNullOrWhiteSpace(email)) payload["email"] = email;

        PostRaw<Dictionary<string, object>, ErrorResponse>(payload);
    }
}