namespace BaladGateway.Models;

public static class Errors
{
	public static readonly Error NO_RECORD_FOUND
		= new(102, "No Record Found", null, null);

	public static readonly Error USER_DOES_NOT_EXIST
		= new(8021, "User dose not exists", null, null);
}

public record Error(
	int Code,
	string Message,
	string? Field,
	string? AdditionalInfo
);
public class ErrorWrapper
{
	public object? Data { get; set; }
	public List<Error>? Errors { get; set; }
}