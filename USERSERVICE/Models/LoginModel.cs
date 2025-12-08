using System.ComponentModel.DataAnnotations;

namespace UserService.Model;

public class LoginModel
{
    [Required(ErrorMessage = "Email or Username is required")]
    public string? Identifier { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string? Password { get; set; }
}