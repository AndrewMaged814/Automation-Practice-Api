using Xunit.Abstractions;

namespace AutomationPractice_api.StepDefinitions.API;

public abstract class APITestBase<TResponse>(
    ITestOutputHelper outputHelper,
    ScenarioContext scenarioContext,
    string testEndpoint)
    : TestBase<TResponse>(testEndpoint, outputHelper, scenarioContext)
{
}