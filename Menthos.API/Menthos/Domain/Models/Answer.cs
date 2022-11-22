namespace Menthos.API.Menthos.Domain.Models;

public class Answer
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    //Relationships
    public  int QuestionId { get; set; }
    public Question Question { get; set; }
}