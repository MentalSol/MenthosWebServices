using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> ListAsync();
    Task<SubjectResponse> SaveAsync(Subject subject);
    Task<SubjectResponse> UpdateAsync(int id, Subject subject);
    Task<SubjectResponse> DeleteAsync(int id);
}