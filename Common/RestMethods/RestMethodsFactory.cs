using Common.Interfaces;

namespace Common.RestMethods;

public static class RestMethodsFactory
{
	public static IRestMethods GetMethods(bool isEncryptionEnabled)
	{
		if (isEncryptionEnabled)
			return EncryptedRestMethods.GetInstance();

		return PlainRestMethods.GetInstance();

	}
}