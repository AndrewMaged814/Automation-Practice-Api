using System.Collections.Generic;
using System.Linq;
using AutomationPractice_api.Models.API;
using AutomationPractice_api.Models.API.GetUser;
using Xunit.Abstractions;

namespace AutomationPractice_api.StepDefinitions.API.GetUser;

[Binding]
public sealed class GetUsersPaginated(ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
    : APITestBase<Paginated<GetSingleUserResponse>>(outputHelper, scenarioContext, _endpoint)

{
    private const string _endpoint = "api/users";

    [When("I request the user paginated list")]
    public void WhenIRequestTheUserPaginatedList()
    {
        Get(queryParams: new Dictionary<string, object>
        {
            { "page", 2 },
        });

    }

    [When("the response should include the following users:")]
    public void WhenTheResponseShouldIncludeTheFollowingUsers(Table table)
    {
        var expectedEmails = table.Rows.Select(r => r["email"]).ToList();

        var actualEmails = Response.Data.Select(u => u.Email).ToList();

        foreach (var expectedEmail in expectedEmails)
        {
            Assert.Contains(expectedEmail, actualEmails);
        }
    }
}