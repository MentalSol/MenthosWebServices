using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Services.Communication;

namespace Menthos.API.Security.Domain.Services;

public interface ITeacherService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<Teacher>> ListAsync();
    Task<Teacher> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}