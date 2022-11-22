using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> ListAsync();
    Task AddAsync(Subject subject);
    Task<Subject> FindByIdAsync(int id);
    void Update(Subject subject);
    void Remove(Subject subject);
}