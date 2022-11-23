using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>> ListAsync();
    Task<Teacher> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}