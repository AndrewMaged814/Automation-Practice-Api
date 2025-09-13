namespace AutomationPractice_api.Models.API.GetUser;

public record GetSingleUserResponse
{
    public int Id { get; init; }
    public string Email { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Avatar { get; init; } = string.Empty;
}

public class SingleUserResponse
{
    public GetSingleUserResponse Data { get; set; } = new();
}