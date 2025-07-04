using Xunit.Abstractions;

namespace ParaBankAppPractice.Features.API;

public abstract class APITestBase<TResponse>(ITestOutputHelper outputHelper, ScenarioContext scenarioContext, string testEndpoint)
	: TestBase<TResponse>(() => scenarioContext.GetContextClient().ApiCredentials.GetToken(), testEndpoint, outputHelper, scenarioContext)
{
}
