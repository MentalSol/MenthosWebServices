using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Menthos.Resources;

public class SaveQuestionResource
{
    [Required]
    [MaxLength(300)]
    public string Content { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int SubjectId { get; set; }
}