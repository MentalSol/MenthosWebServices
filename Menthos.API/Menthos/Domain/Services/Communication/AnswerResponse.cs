using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class AnswerResponse : BaseResponse<Answer>
{
    public AnswerResponse(string message) : base(message)
    {
    }

    public AnswerResponse(Answer resource) : base(resource)
    {
    }
}