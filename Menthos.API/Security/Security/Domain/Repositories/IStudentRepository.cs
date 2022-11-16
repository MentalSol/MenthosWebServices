using Menthos.API.Security.Domain.Models;

namespace Menthos.API.Security.Domain.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> ListAsync();
    Task AddAsync(Student student);
    Task<Student> FindByIdAsync(int id);
    Task<Student> FindByUsernameAsync(string username);
    public bool ExistsByUsername(string username);
    Student FindById(int id);
    void Update(Student student);
    void Remove(Student student);
}