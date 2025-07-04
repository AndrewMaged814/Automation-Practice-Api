using System.Collections.Generic;
using ParaBankAppPractice.Models.API.GetUser;
using ParaBankAppPractice.Utils;
using Xunit.Abstractions;

namespace ParaBankAppPractice.StepDefinitions.API.GetUser;

[Binding]
public sealed class GetUserSteps(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<UserData>(outputHelper, scenarioContext, _endpoint)

{
    private const string _endpoint = "/api/user/{user_id}";


    [Then("I fetch the newly registered user by ID")]
    public void ThenIFetchTheNewlyRegisteredUserById()
    {
        Get(pathParams: new Dictionary<string, object>
        {
            { "user_id", scenarioContext.GetValue<int>(ScenarioContextKeys.UserId) }
        });
    }
}