using Menthos.API.Security.Domain.Models;

namespace Menthos.API.Menthos.Domain.Models;

public class Video
{
    public int Id { get; set; }
    public string Link { get; set; }
    
    //Relationships
    
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    
    public int SubjectId { get; set; }
    
    public Subject Subject { get; set; }
    
    public int TeacherId { get; set; }
    
    public Teacher Teacher { get; set; }
}