using System;
using System.Collections.Generic;
using System.Net;
using Common.Extensions;
using AutomationPractice_ui_api.Models;

namespace AutomationPractice_ui_api.Utils;

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