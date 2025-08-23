using bookFlow.Enum;
using System.Text.Json.Serialization;

namespace bookFlow.Dto
{
    public class RegistryDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ERole Role { get; set; } = ERole.USER; // default to USER
    }
}
