using System.Net;

namespace Common.Interfaces;

public interface IResponse
{
	HttpStatusCode GetStatusCode();
	T GetBody<T>();
	string GetHeader(string header);
}