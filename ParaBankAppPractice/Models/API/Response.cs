using System.Collections.Generic;

namespace ParaBankAppPractice.Models;

public interface IResponse<out T>
{
	T? Data { get; }
}

public record Response<T>(
	T? Data
) : IResponse<T>;
