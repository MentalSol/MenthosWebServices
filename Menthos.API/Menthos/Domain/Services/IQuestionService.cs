using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services;

public interface IQuestionService
{
    Task<IEnumerable<Question>> ListAsync();
    Task<QuestionResponse> SaveAsync(Question question);
    Task<QuestionResponse> UpdateAsync(int questionId, Question question);
    Task<QuestionResponse> DeleteAsync(int questionId);
}