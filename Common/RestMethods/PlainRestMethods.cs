using System.Collections.Generic;
using Common.Extensions;
using Common.Interfaces;
using Common.Utils;
using RestSharp;
using static Common.Utils.Constants;

namespace Common.RestMethods;

public class PlainRestMethods : IRestMethods
{
    private static PlainRestMethods? _plainRestMethods;
    private static JsonConverter? _jsonConverter;
    private readonly string BaseUrl = BASE_URL!;

    private PlainRestMethods()
    {
    }

    public static PlainRestMethods GetInstance()
    {
        _jsonConverter = new JsonConverter();
        return _plainRestMethods ??= new();
    }

    public IResponse Get<T>(string endpoint,
        Dictionary<string, object>? pathParams = null,
        Dictionary<string, object>? queryParams = null)
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest(endpoint);
        
        
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "*/*");
        request.AddHeader("x-api-key", "reqres-free-v1"); 

        pathParams?.ForEach(param => request.AddUrlSegment(param.Key, param.Value.ToString()!));
        queryParams?.ForEach(param => request.AddQueryParameter(param.Key, param.Value.ToString()));
        return new PlainResponse(client.Execute<T>(request));
    }

    public IResponse PostWithBody<T>(string endpoint,
        Dictionary<string, object>? pathParams = null,
        T? body = default)
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest(endpoint, Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "*/*");
        request.AddHeader("x-api-key", "reqres-free-v1"); 


        pathParams?.ForEach(param => request.AddUrlSegment(param.Key, param.Value.ToString()!));
        request.AddBody(_jsonConverter!.ToJson<T>(body!));
        return new PlainResponse(client.Execute<T>(request));
    }

    public IResponse PostWithParams<T>(string endpoint,
        Dictionary<string, object>? pathParams = null,
        Dictionary<string, object>? formParams = null)
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest(endpoint, Method.Post);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Accept", "*/*");
        pathParams?.ForEach(param => request.AddUrlSegment(param.Key, param.Value.ToString()!));
        formParams?.ForEach(param => request.AddParameter(param.Key, param.Value.ToString()));
        return new PlainResponse(client.Execute<T>(request));
    }

    public IResponse PostWithMultiParts<T>(string endpoint,
        Dictionary<string, object>? pathParams = null,
        Dictionary<string, object>? formParams = null,
        Dictionary<string, object>? multiParts = null)
    {
        var client = new RestClient(BaseUrl);
        var request = new RestRequest(endpoint, Method.Post);
        request.AddHeader("Content-Type", "multipart/form-data");
        request.AddHeader("Accept", "*/*");
        pathParams?.ForEach(param => request.AddUrlSegment(param.Key, param.Value.ToString()!));
        formParams?.ForEach(param => request.AddParameter(param.Key, param.Value.ToString()));
        multiParts?.ForEach(param => request.AddFile(param.Key, param.Value.ToString()!));
        return new PlainResponse(client.Execute<T>(request));
    }
}