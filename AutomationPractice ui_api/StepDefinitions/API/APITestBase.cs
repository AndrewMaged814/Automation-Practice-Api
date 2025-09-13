using Xunit.Abstractions;

namespace AutomationPractice_ui_api.StepDefinitions.API;

public abstract class APITestBase<TResponse>(
    ITestOutputHelper outputHelper,
    ScenarioContext scenarioContext,
    string testEndpoint)
    : TestBase<TResponse>(testEndpoint, outputHelper, scenarioContext)
{
}