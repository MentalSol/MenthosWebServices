using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class StudentResponse : BaseResponse<Student>
{
    public StudentResponse(string message) : base(message)
    {
    }

    public StudentResponse(Student resource) : base(resource)
    {
    }
}