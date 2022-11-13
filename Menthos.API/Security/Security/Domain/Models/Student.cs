using System.Text.Json.Serialization;

namespace Menthos.API.Security.Domain.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Codigo { get; set; }
    
    [JsonIgnore]
    public string PasswordHash { get; set; }
}