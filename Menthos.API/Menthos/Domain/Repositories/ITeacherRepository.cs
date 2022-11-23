using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> ListAsync();
    Task AddAsync(Teacher teacher);
    Task<Teacher> FindByIdAsync(int id);
    Task<Teacher> FindByUsernameAsync(string username);
    public bool ExistsByUsername(string username);
    Teacher FindById(int id);
    void Update(Teacher teacher);
    void Remove(Teacher teacher);
}