using Common.Interfaces;
using Common.Utils;
using RestSharp;
using System.Net;

namespace Common.RestMethods;

public class PlainResponse : IResponse
{
	private readonly RestResponse response;
	private readonly JsonConverter _converter;

	public PlainResponse(RestResponse response)
	{
		this.response = response;
		_converter = new JsonConverter();
	}
	public Uri GetResponseUri()
	{
		return response.ResponseUri!;
	}


	public HttpStatusCode GetStatusCode()
	{
		return response.StatusCode;
	}

	public T GetBody<T>()
	{
		return _converter.FromJson<T>(response.Content!);
	}

	public string GetHeader(string header)
	{
		foreach (var headerParameter in response.Headers!)
			if (headerParameter.Name!.Equals(header, StringComparison.OrdinalIgnoreCase))
				return headerParameter.Value;

		return string.Empty;
	}
}