using BaladGateway.Models;
using Common.Extensions;
using System.Net;

namespace BaladGateway.Utils;

public static class AssertHelper
{
	public static void AssertError(Table expected, Error actualError)
	{
		var expectedError = expected.ConvertTo<Error>();
		Assert.Equal(expectedError, actualError);
	}
	public static void AssertErrorCode(Table expected, Error actualError)
	{
		var expectedError = expected.ConvertTo<Error>();
		Assert.Equal(expectedError.Code, actualError.Code);
	}
	public static void SoftAssert(params Action[] assertions)
	{
		List<Exception> _exceptions = [];
		foreach (var assert in assertions)
		{
			try
			{
				assert();
			}
			catch (Exception ex)
			{
				_exceptions.Add(ex);
			}
		}

		if (_exceptions.Count > 0)
		{
			throw new AggregateException("There were assertion failures:", _exceptions);
		}
	}
	public static void AssertSuccessResponse<T>(HttpStatusCode statusCode, IResponse<T>? response)
	{
		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.NotNull(response);
		Assert.Null(response.Errors);
		Assert.NotNull(response.Data);
	}
	public static void AssertSuccessEmptyResponse<T>(HttpStatusCode statusCode, IResponse<T>? response)
	{
		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.NotNull(response);
		Assert.Null(response.Errors);
		Assert.Null(response.Data);
	}
	public static void AssertErrorResponse<T>(HttpStatusCode statusCode, IResponse<T>? response, Table expectedError)
	{
		AssertErrorResponse(statusCode, response);
		AssertError(expectedError, response!.Errors[0]);
	}
	public static void AssertErrorResponse<T>(HttpStatusCode statusCode, IResponse<T>? response, Error expectedError)
	{
		AssertErrorResponse(statusCode, response);
		Assert.Equal(expectedError, response!.Errors[0]);
	}
	private static void AssertErrorResponse<T>(HttpStatusCode statusCode, IResponse<T>? response)
	{
		Assert.Equal(HttpStatusCode.BadRequest, statusCode);
		Assert.Null(response!.Data);
		Assert.Single(response!.Errors);
	}
}