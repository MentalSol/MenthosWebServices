using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Models;

public class Question
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    //Relationships

    public IList<Answer> Answers { get; set; } = new List<Answer>();
    
    public int StudentId { get; set; }
    
    public Student Student { get; set; }
    
    public int SubjectId { get; set; }
    
    public Subject Subject { get; set; }
}