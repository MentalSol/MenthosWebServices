using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class TeacherResponse : BaseResponse<Teacher>
{
    public TeacherResponse(string message) : base(message)
    {
    }

    public TeacherResponse(Teacher resource) : base(resource)
    {
    }
}