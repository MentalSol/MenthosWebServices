using System.ComponentModel.DataAnnotations;

namespace Menthos.API.Menthos.Resources;

public class SaveAnswerResource
{
    [Required]
    public string Content { get; set; }
    
    [Required]
    public int QuestionId { get; set; }
}