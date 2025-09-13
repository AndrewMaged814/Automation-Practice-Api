using System.Collections.Generic;
using System.Net;
using Common.Extensions;
using Common.Interfaces;
using Common.Logger;
using Common.RestMethods;
using AutomationPractice_ui_api.Utils;
using Serilog;
using Xunit.Abstractions;

namespace AutomationPractice_ui_api.StepDefinitions
{
    public abstract class TestBase<TResponse>(
        string testEndpoint,
        ITestOutputHelper outputHelper,
        ScenarioContext scenarioContext)
    {
        protected readonly ILogger logger = LoggerFactory.GetLogger(outputHelper);
        protected readonly ScenarioContext scenarioContext = scenarioContext;
        protected readonly IRestMethods methods = RestMethodsFactory.GetMethods();

        protected TResponse Response =>
            scenarioContext.Get<TResponse>(ScenarioContextKeys.ResponseKey);

        protected HttpStatusCode StatusCode =>
            scenarioContext.Get<HttpStatusCode>(ScenarioContextKeys.ResponseStatusCodeKey);

        // === GET request ===
        public void Get(
            Dictionary<string, object>? queryParams = null,
            Dictionary<string, object>? pathParams = null)
        {
            var response = methods.Get<IResponse>(testEndpoint, queryParams: queryParams, pathParams: pathParams);
            SetContextData(response);
        }

        // === POST with expected wrapped response ===
        public void Post<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
        {
            logger.LogDebug(request.AsJson());
            var response = methods.PostWithBody(testEndpoint, body: request, pathParams: pathParams);
            SetContextData(response);
        }

        // === POST with expected raw/unwrapped response ===
        public void PostRaw<TRequest>(TRequest request, Dictionary<string, object>? pathParams = null)
        {
            logger.LogDebug(request.AsJson());
            var response = methods.PostWithBody(testEndpoint, body: request, pathParams: pathParams);
            SetRawContextData(response);
        }

        private void SetContextData(IResponse response)
        {
            var statusCode = response.GetStatusCode();
            // deserialize directly into TResponse
            var responseBody = response.GetBody<TResponse>();

            LogDebug(response, statusCode, responseBody);

            scenarioContext[ScenarioContextKeys.ResponseKey] = responseBody!;
            scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = statusCode;
        }

        private void SetRawContextData(IResponse response)
        {
            var statusCode = response.GetStatusCode();
            var responseBody = response.GetBody<TResponse>();

            LogDebug(response, statusCode, responseBody);

            scenarioContext[ScenarioContextKeys.ResponseKey] = responseBody!;
            scenarioContext[ScenarioContextKeys.ResponseStatusCodeKey] = statusCode;
        }

        private void LogDebug(IResponse response, HttpStatusCode statusCode, TResponse body)
        {
            var raw = response.GetRawBody();
            logger.LogDebug("Raw response: " + raw);
            logger.LogDebug(statusCode.ToString());
            logger.LogDebug(body!.AsJson());
            if (response is PlainResponse plain)
            {
                logger.LogDebug($"Full Request URL: {plain.GetResponseUri()}");
            }
        }
    }
}
