using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Menthos.Resources;

public class SaveVideoResource
{
    [Required]
    public string Link { get; set; }
    
    [Required]
    public int SubjectId { get; set; }
    
    [Required]
    public int TeacherId { get; set; }
}