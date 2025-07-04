using BaladGateway.Models;
using System.Net;

namespace BaladGateway.StepDefinitions;

[Binding]
public class CommonErrorsTest(ScenarioContext scenarioContext)
{
	private readonly ScenarioContext _scenarioContext = scenarioContext;

	[Then("User receives an error")]
	public void ThenUserReceivesAnError(Table error)
	{
		var statusCode = _scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
		var response = _scenarioContext.Get<IResponse<object>>(ScenarioContextKeys.ResponseKey);

		AssertHelper.AssertErrorResponse(statusCode, response, error);
	}

	[Then("User receives Empty List")]
	public void ThenBaladLinkUserReceivesEmptyList()
	{
		var statusCode = _scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
		var response = _scenarioContext.Get<IResponse<IEnumerable<object>>>(ScenarioContextKeys.ResponseKey);

		AssertHelper.AssertSuccessResponse(statusCode, response);
		Assert.Empty(response.Data);
	}

	[Then("User should receives no record found response")]
	public void ThenBaladLinkUserReceivesNoRecordFoundResponse()
	{
		var statusCode = _scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
		var response = _scenarioContext.Get<IResponse<object>>(ScenarioContextKeys.ResponseKey);

		AssertHelper.AssertErrorResponse(statusCode, response, Errors.NO_RECORD_FOUND);
	}

	[Then("User receives an error code")]
	public void ThenUserReceivesAnErrorCode(Table error)
	{
		var statusCode = _scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);
		var response = _scenarioContext.Get<IResponse<object>>(ScenarioContextKeys.ResponseKey);

		Assert.Equal(HttpStatusCode.BadRequest, statusCode);
		AssertHelper.AssertErrorCode(error, response!.Errors[0]);
	}
}