using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> ListAsync();
    Task AddAsync(Question question);
    Task<Question> FindByIdAsync(int questionId);
    Task<Question> FindByContentAsync(string content);
    void Update(Question question);
    void Remove(Question question);
}