using System.Collections.Generic;
using System.Net;
using AutomationPractice_ui_api.Utils;
using Common.Extensions;
using Common.Interfaces;
using Common.Logger;
using Common.RestMethods;
using Serilog;
using Xunit.Abstractions;

namespace AutomationPractice_api.StepDefinitions.API
{
    public abstract class TestBase<TResponse>(
        string testEndpoint,
        ITestOutputHelper outputHelper,
        ScenarioContext scenarioContext)
    {
        private readonly ILogger _logger = LoggerFactory.GetLogger(outputHelper);
        protected readonly ScenarioContext ScenarioContext = scenarioContext;
        private readonly IRestMethods _methods = RestMethodsFactory.GetMethods();

        protected TResponse Response =>
            ScenarioContext.Get<TResponse>(ScenarioContextKeys.ResponseKey);

        protected HttpStatusCode StatusCode =>
            ScenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

        // === GET request ===
        protected void Get(
            Dictionary<string, object>? queryParams = null,
            Dictionary<string, object>? pathParams = null)
        {
            var response = _methods.Get<IResponse>(testEndpoint, queryParams: queryParams, pathParams: pathParams);
            SetContextData(response);
        }

        // === POST with expected wrapped response ===
        protected void Post<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
        {
            _logger.LogDebug(request.AsJson());
            var response = _methods.PostWithBody(testEndpoint, body: request, pathParams: pathParams);
            SetContextData(response);
        }
        
        private void SetContextData(IResponse response)
        {
            var statusCode = response.GetStatusCode();
            // deserialize directly into TResponse
            var responseBody = response.GetBody<TResponse>();

            LogDebug(response, statusCode, responseBody);

            ScenarioContext[ScenarioContextKeys.ResponseKey] = responseBody!;
            ScenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = statusCode;
        }

        private void LogDebug(IResponse response, HttpStatusCode statusCode, TResponse body)
        {
            var raw = response.GetRawBody();
            _logger.LogDebug("Raw response: " + raw);
            _logger.LogDebug(statusCode.ToString());
            _logger.LogDebug(body!.AsJson());
            if (response is PlainResponse plain)
            {
                _logger.LogDebug($"Full Request URL: {plain.GetResponseUri()}");
            }
        }
    }
}
