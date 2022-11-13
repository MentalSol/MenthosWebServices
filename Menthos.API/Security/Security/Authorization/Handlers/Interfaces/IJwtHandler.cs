using Menthos.API.Security.Domain.Models;

namespace Menthos.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(Student student);
    public string GenerateToken(Teacher teacher);
    
    public int? ValidateToken(string token);
}