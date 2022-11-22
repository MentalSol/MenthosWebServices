using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/students/{studentId}/questions")]
public class StudentQuestionsController
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;

    public StudentQuestionsController(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Questions for given Question",
        Description = "Get existing Questions associated with the specified Student",
        OperationId = "GetStudentQuestions",
        Tags = new[] { "Student"}
    )]
    public async Task<IEnumerable<QuestionResource>> GetAllByStudentIdAsync(int studentId)
    {
        var questions = await _questionService.ListByStudentIdAsync(studentId);

        var resources = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionResource>>(questions);

        return resources;
    }
}