using System.ComponentModel.DataAnnotations;

namespace T2507E_ASP.DTOs.Auth;

public class LoginRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}