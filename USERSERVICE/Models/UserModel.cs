using System.ComponentModel.DataAnnotations;

namespace UserService.Model;

public class UserModel
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Number { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? CoverImageUrl { get; set; }
    public bool IsVerfiled { get; set; }
    public bool CreatedAt { get; set; }
    public bool UpdatedAt { get; set; }

}