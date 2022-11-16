using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Lastname { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}