using System;
using System.Collections.Generic;
using System.Net;
using Common.Extensions;
using ParaBankAppPractice.Models;

namespace ParaBankAppPractice.Utils;

public static class AssertHelper
{
	public static void AssertSuccessResponse(HttpStatusCode statusCode, object responseBody)
	{
		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.NotNull(responseBody);
	}
	public static void AssertSuccessEmptyResponse(HttpStatusCode statusCode, object responseBody)
	{
		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.Null(responseBody);  
	}

	
}