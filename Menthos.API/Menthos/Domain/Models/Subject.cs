namespace Menthos.API.Menthos.Domain.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    
    //Relationships

    public IList<Question> Questions { get; set; } = new List<Question>();
    public IList<Video> Videos { get; set; } = new List<Video>();
}