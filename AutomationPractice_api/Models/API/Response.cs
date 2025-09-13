namespace AutomationPractice_api.Models.API;

public interface IResponse<out T>
{
	T? Data { get; }
}

public record Response<T>(
	T? Data
) : IResponse<T>;
