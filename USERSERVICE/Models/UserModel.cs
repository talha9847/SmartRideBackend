using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserService.Model;

public class UserModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Invalid Email Format")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [Phone(ErrorMessage = "Invalid mobile format")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Role { get; set; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsVerified { get; set; } = false;
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}