using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/subjects/{subjectId}/questions")]
public class SubjectQuestionsController
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;
    
    public SubjectQuestionsController(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Questions for given Question",
        Description = "Get existing Questions associated with the specified Subject",
        OperationId = "GetSubjectQuestions",
        Tags = new[] { "Subjects"}
    )]
    public async Task<IEnumerable<QuestionResource>> GetAllBySubjectIdAsync(int studentId)
    {
        var questions = await _questionService.ListBySubjectIdAsync(studentId);

        var resources = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionResource>>(questions);

        return resources;
    }
}