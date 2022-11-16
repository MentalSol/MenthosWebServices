using AutoMapper;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Services.Communication;

namespace Menthos.API.Security.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, Student>();

        CreateMap<UpdateRequest, Student>()
            .ForAllMembers(options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
            ));
        
        CreateMap<RegisterRequest, Teacher>();

        CreateMap<UpdateRequest, Teacher>()
            .ForAllMembers(options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
            ));
    }
}