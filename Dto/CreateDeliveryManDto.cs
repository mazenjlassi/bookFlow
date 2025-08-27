using System.ComponentModel.DataAnnotations;

public class CreateDeliveryManDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string PhoneNumber { get; set; }   

    [Required]
    public string VehicleNumber { get; set; }   
}
