using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Menthos.Resources;

public class SaveStudentResource
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Codigo { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public int telephone { get; set; }
}