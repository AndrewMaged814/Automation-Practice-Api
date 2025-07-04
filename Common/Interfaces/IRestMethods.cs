using System.Collections.Generic;

namespace Common.Interfaces;

public interface IRestMethods
{
	public IResponse Get<T>(string endpoint,
		Dictionary<string, object>? pathParams = null,
		Dictionary<string, object>? queryParams = null);

	public IResponse PostWithBody<T>(string endpoint,
		Dictionary<string, object>? pathParams = null,
		T? body = default);

	public IResponse PostWithParams<T>(string endpoint,
		Dictionary<string, object>? pathParams = null,
		Dictionary<string, object>? formParams = null);

	public IResponse PostWithMultiParts<T>(string endpoint,
		Dictionary<string, object>? pathParams = null,
		Dictionary<string, object>? formParams = null,
		Dictionary<string, object>? multiParts = null);
}