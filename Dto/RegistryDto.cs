using System.ComponentModel.DataAnnotations;

namespace bookFlow.Dto
{
    public class RegistryDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string Email {  get; set; } = string.Empty;
        [Required]
        public string FullName {  get; set; } = string.Empty;
    }
}
