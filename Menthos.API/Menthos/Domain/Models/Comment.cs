using Menthos.API.Security.Domain.Models;

namespace Menthos.API.Menthos.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string MessageC { get; set; }
    
    //Relationships
    
    public int VideoId { get; set; }
    
    public Video Video { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; }
    
}