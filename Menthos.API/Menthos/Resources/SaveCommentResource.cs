using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Menthos.Resources;

public class SaveCommentResource
{
    [Required]
    [MaxLength(200)]
    public string MessageC { get; set; }
    
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int VideoId { get; set; }
}