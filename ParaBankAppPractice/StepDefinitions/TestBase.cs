using System.Collections.Generic;
using System.Net;
using Common.Extensions;
using Common.Interfaces;
using Common.Logger;
using Common.RestMethods;
using ParaBankAppPractice.Models;
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

    protected Response<TResponse> Response => scenarioContext.Get<Response<TResponse>>(ScenarioContextKeys.ResponseKey);
    protected HttpStatusCode StatusCode => scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

    // === POST with expected wrapped response ===
    public void Post<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
    {
        logger.LogDebug(request.AsJson());
        var response = methods.PostWithBody(testEndpoint, body: request, pathParams: pathParams);
        SetContextData(response);
    }

    // === POST with expected raw/unwrapped response ===
    public void PostRaw<TRequest, TRawResponse>(TRequest request, Dictionary<string, object>? pathParams = null)
    {
        logger.LogDebug(request.AsJson());
        var response = methods.PostWithBody(testEndpoint, body: request, pathParams: pathParams);
        SetRawContextData<TRawResponse>(response);
    }

    // === Multipart form POST ===
    public void PostForm(
        Dictionary<string, object>? formParams = null,
        Dictionary<string, object>? pathParams = null,
        Dictionary<string, object>? multiParts = null)
    {
        logger.LogDebug(formParams.AsJson());
        var response = methods.PostWithMultiParts<IResponse>(testEndpoint, pathParams: pathParams, formParams: formParams, multiParts: multiParts);
        SetContextData(response);
    }

    // === GET request ===
    public void Get(Dictionary<string, object>? queryParams = null, Dictionary<string, object>? pathParams = null)
    {
        var response = methods.Get<IResponse>(testEndpoint, queryParams: queryParams, pathParams: pathParams);
        SetContextData(response);
    }

    // === Handles wrapped response ===
    public void SetContextData(IResponse response)
    {
        var statusCode = response.GetStatusCode();
        var responseBody = response.GetBody<Response<TResponse>>();

        logger.LogDebug(statusCode.ToString());
        logger.LogDebug(responseBody.AsJson());

        if (response is PlainResponse plainResponse)
        {
            logger.LogDebug($"Full Request URL: {plainResponse.GetResponseUri()}");
        }

        scenarioContext[ScenarioContextKeys.ResponseKey] = responseBody;
        scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = statusCode;
    }

    // === Handles raw/unwrapped response ===
    public void SetRawContextData<TRaw>(IResponse response)
    {
        var statusCode = response.GetStatusCode();
        var responseBody = response.GetBody<TRaw>();

        logger.LogDebug(statusCode.ToString());
        logger.LogDebug(responseBody.AsJson());

        if (response is PlainResponse plainResponse)
        {
            logger.LogDebug($"Full Request URL: {plainResponse.GetResponseUri()}");
        }

        scenarioContext[ScenarioContextKeys.ResponseKey] = responseBody;
        scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = statusCode;
    }
}
