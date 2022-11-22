using Menthos.API.Menthos.Domain.Models;

namespace Menthos.API.Menthos.Domain.Repositories;

public interface IAnswerRepository
{
    Task<IEnumerable<Answer>> ListAsync();
    Task AddAsync(Answer answer);
    Task<Answer> FindByIdAsync(int answerId);
    Task<Answer> FindByContentAsync(string content);
    Task<IEnumerable<Answer>> FindByQuestionIdAsync(int questionId);
    void Update(Answer answer);
    void Remove(Answer answer);
}