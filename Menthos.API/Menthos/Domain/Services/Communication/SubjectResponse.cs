using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class SubjectResponse : BaseResponse<Subject>
{
    public SubjectResponse(string message) : base(message)
    {
    }

    public SubjectResponse(Subject resource) : base(resource)
    {
    }
}