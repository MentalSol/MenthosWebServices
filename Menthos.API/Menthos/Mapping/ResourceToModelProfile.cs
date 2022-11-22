using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Resources;

namespace Menthos.API.Menthos.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveQuestionResource, Question>();
        CreateMap<SaveAnswerResource, Answer>();
        CreateMap<SaveSubjectResource, Subject>();
        CreateMap<SaveVideoResource, Video>();
        CreateMap<SaveCommentResource, Comment>();
    }
}