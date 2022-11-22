using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services;

public interface IAnswerService
{
    Task<IEnumerable<Answer>> ListAsync();
    Task<IEnumerable<Answer>> ListByQuestionIdAsync(int questionId);
    Task<AnswerResponse> SaveAsync(Answer answer);
    Task<AnswerResponse> UpdateAsync(int answerId, Answer answer);
    Task<AnswerResponse> DeleteAsync(int answerId);
}