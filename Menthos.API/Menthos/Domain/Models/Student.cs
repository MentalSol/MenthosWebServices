using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set;  }
    public string Username { get; set; }
    public string Codigo { get; set; }
    public string email { get; set; }
    public int telephone { get; set; }
    
    public string password { get; set; }
    
    // Relationships

    public IList<Question> Questions { get; set; } = new List<Question>();

    public IList<Comment> Comments { get; set; } = new List<Comment>();
    
}