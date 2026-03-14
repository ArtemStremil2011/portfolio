using System.ComponentModel.DataAnnotations;

public class RegisterCustomerDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [MaxLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-']+$", ErrorMessage = "First name can only contain letters, spaces, hyphens and apostrophes")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
    [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-']+$", ErrorMessage = "Last name can only contain letters, spaces, hyphens and apostrophes")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Please enter a valid phone number in international format (e.g., +1234567890)")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}