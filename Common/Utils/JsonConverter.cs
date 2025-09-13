using System;
using System.Text.Json;

namespace Common.Utils;

public class JsonConverter
{
	private readonly JsonSerializerOptions _options = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
		PropertyNameCaseInsensitive = true
	};

	public T FromJson<T>(string json)
	{
		var body = default(T)!;
		try
		{
			return JsonSerializer.Deserialize<T>(json, _options)!;
		}
		catch (Exception e) when (e is JsonException)
		{
		}

		return body;
	}
	public string ToJson<T>(T type)
	{
		var body = string.Empty;
		try
		{
			return JsonSerializer.Serialize(type, _options);
		}
		catch (Exception e) when (e is JsonException)
		{
		}

		return body;
	}
}
