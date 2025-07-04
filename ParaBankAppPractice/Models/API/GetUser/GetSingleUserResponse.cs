namespace ParaBankAppPractice.Models.API.GetUser;

public class GetSingleUserResponse
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;


    public string LastName { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
}