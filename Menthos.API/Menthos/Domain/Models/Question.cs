namespace Menthos.API.Menthos.Domain.Models;

public class Question
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    //Relationships

    public IList<Answer> Answers { get; set; } = new List<Answer>();

}