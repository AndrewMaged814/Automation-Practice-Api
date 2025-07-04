using System.Collections.Generic;
using System.Net;
using Common.Extensions;
using Common.Interfaces;
using Common.Logger;
using Common.RestMethods;
using ParaBankAppPractice.Utils;
using Serilog;
using Xunit.Abstractions;

namespace ParaBankAppPractice.StepDefinitions;

public abstract class TestBase<TResponse>(
    string testEndpoint,
    ITestOutputHelper outputHelper,
    ScenarioContext scenarioContext)
{
    protected readonly ILogger logger = LoggerFactory.GetLogger(outputHelper);
    protected readonly ScenarioContext scenarioContext = scenarioContext;
    protected readonly IRestMethods methods = RestMethodsFactory.GetMethods();

    protected TResponse Response => scenarioContext.Get<TResponse>(ScenarioContextKeys.ResponseKey);
    // protected Response<TResponse> ResponseData => scenarioContext.Get<Response<TResponse>>(ScenarioContextKeys.ResponseKey);

    protected HttpStatusCode StatusCode =>
        scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

    public void Post<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
    {
        var response = methods.PostWithBody(
            testEndpoint,
            pathParams,
            request
        );
        SetContextData(response);
    }

    public void Get(Dictionary<string, object>? queryParams = null, Dictionary<string, object>? pathParams = null)
    {
        var response = methods.Get<IResponse>(
            testEndpoint,
            pathParams,
            queryParams
        );
        SetContextData(response);
    }

    private void SetContextData(IResponse response)
    {
        if (response is PlainResponse plainResponse)
            logger.LogDebug($"Full Request URL: {plainResponse.GetResponseUri()}");

        var httpStatusCode = response!.GetStatusCode();
        var body = response.GetBody<TResponse>();

        logger.LogDebug(httpStatusCode.ToString());
        logger.LogDebug(body.AsJson());


        scenarioContext[ScenarioContextKeys.ResponseKey] = body;
        scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = httpStatusCode;
    }
}