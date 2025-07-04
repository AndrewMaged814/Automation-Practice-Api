using Common.Interfaces;

namespace Common.RestMethods;

public static class RestMethodsFactory
{
	public static IRestMethods GetMethods()
	{
		return PlainRestMethods.GetInstance();
	}
}