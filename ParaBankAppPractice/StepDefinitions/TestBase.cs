using System.Net;
using BaladGateway.Models;
using Common.Extensions;
using Common.Interfaces;
using Common.Logger;
using Common.RestMethods;
using Serilog;
using Xunit.Abstractions;

namespace BaladGateway.StepDefinitions;

public abstract class TestBase<TResponse>(Func<string> getToken, string testEndpoint, ITestOutputHelper outputHelper, ScenarioContext scenarioContext)
{
    protected readonly ILogger logger = LoggerFactory.GetLogger(outputHelper);
    protected readonly ScenarioContext scenarioContext = scenarioContext;
    protected readonly IRestMethods methods = RestMethodsFactory.GetMethods(scenarioContext.GetContextClient().ClientEncryption.Enabled);
    protected readonly Generator gen = new();

    protected Response<TResponse> Response => scenarioContext.Get<Response<TResponse>>(ScenarioContextKeys.ResponseKey);
    protected HttpStatusCode StatusCode => scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

    public void Post<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
    {
        logger.LogDebug(request.AsJson());
        var token = getToken();
        var _response = methods.PostWithBody(testEndpoint, getToken(), body: request, pathParams: pathParams);
        SetContextData(_response);
    }

    public void PostForm(
        Dictionary<string, object>? formParams = null,
        Dictionary<string, object>? pathParams = null,
        Dictionary<string, object>? multiParts = null)
    {
        logger.LogDebug(formParams.AsJson());
        var _response = methods.PostWithMultiParts<IResponse>(testEndpoint, getToken(), pathParams: pathParams, formParams: formParams, multiParts: multiParts);
        SetContextData(_response);
    }

    public void Get(Dictionary<string, object>? queryParams = null, Dictionary<string, object>? pathParams = null)
    {
        var _response = methods.Get<IResponse>(testEndpoint, getToken(), queryParams: queryParams, pathParams: pathParams);
        SetContextData(_response);
    }

    public void SetContextData(IResponse response)
    {
        var _httpStatusCode = response!.GetStatusCode();
        var _responseBody = response.GetBody<Response<TResponse>>();

        logger.LogDebug(response.GetStatusCode().ToString());
        logger.LogDebug(response.GetBody<Response<TResponse>>().AsJson());

        if (response is PlainResponse plainResponse)
        {
            logger.LogDebug($"Full Request URL: {plainResponse.GetResponseUri()}");
        }
        
        scenarioContext[ScenarioContextKeys.ResponseKey] = _responseBody;
        scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = _httpStatusCode;
    }

}
