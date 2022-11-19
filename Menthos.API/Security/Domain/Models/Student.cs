using System.Text.Json.Serialization;
using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Security.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set;  }
    public string Username { get; set; }
    public string Codigo { get; set; }
    public string email { get; set; }
    public int telephone { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}