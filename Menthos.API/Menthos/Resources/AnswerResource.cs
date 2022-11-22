using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Resources;

public class AnswerResource
{
    public int Id { get; set; }
    public string Content { get; set; }
    public QuestionResource Question { get; set; }
}