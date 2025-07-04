namespace ParaBankAppPractice.Models.API.Register;

public record RegisterResponse
{
    public int Id { get; init; }
    public string Token { get; init; }
}
