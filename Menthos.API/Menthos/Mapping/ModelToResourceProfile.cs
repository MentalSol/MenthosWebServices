using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services.Communication;
using Menthos.API.Menthos.Resources;

namespace Menthos.API.Menthos.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Question, QuestionResource>();
        CreateMap<Answer, AnswerResource>();
        CreateMap<Subject, SubjectResource>();
        CreateMap<Video, VideoResource>();
    }
}