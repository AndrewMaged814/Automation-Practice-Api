using System.Text.Json.Serialization;

namespace ParaBankAppPractice.Models.API.GetUser
{
    public class GetSingleUser
    {
        public int Id { get; set; }
        
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        
        public string Avatar { get; set; } = string.Empty;
    }
}