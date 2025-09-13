using System.Collections.Generic;

namespace AutomationPractice_ui_api.Models;

public interface IResponse<out T>
{
	T? Data { get; }
}

public record Response<T>(
	T? Data
) : IResponse<T>;
