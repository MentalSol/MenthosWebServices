using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class QuestionResponse : BaseResponse<Question>
{
    public QuestionResponse(string message) : base(message)
    {
    }

    public QuestionResponse(Question resource) : base(resource)
    {
    }
}