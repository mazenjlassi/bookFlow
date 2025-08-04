using System.ComponentModel.DataAnnotations;

namespace bookFlow.Dto
{
    public class UpdateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
