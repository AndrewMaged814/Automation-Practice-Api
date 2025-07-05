using Xunit.Abstractions;

namespace ApiReqresPractice.StepDefinitions.API;

public abstract class APITestBase<TResponse>(
    ITestOutputHelper outputHelper,
    ScenarioContext scenarioContext,
    string testEndpoint)
    : TestBase<TResponse>(testEndpoint, outputHelper, scenarioContext)
{
}