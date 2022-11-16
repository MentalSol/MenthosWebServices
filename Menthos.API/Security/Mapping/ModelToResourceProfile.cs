using AutoMapper;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Services.Communication;
using Menthos.API.Security.Resources;

namespace Menthos.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Student, AuthenticateResponse>();

        CreateMap<Student, StudentResource>();

        CreateMap<Teacher, AuthenticateResponse>();

        CreateMap<Teacher, TeacherResource>();
    }
}