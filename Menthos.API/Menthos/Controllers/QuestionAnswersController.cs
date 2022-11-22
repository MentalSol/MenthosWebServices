using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/questions/{questionId}/answers")]
public class QuestionAnswersController : ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly IMapper _mapper;

    public QuestionAnswersController(IAnswerService answerService, IMapper mapper)
    {
        _answerService = answerService;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Tutorials for given Question",
        Description = "Get existing Answers associated with the specified Question",
        OperationId = "GetQuestionAnswers",
        Tags = new[] { "Questions" }
    )]
    public async Task<IEnumerable<AnswerResource>> GetAllByQuestionIdAsync(int questionId)
    {
        var answers = await _answerService.ListByQuestionIdAsync(questionId);

        var resources = _mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerResource>>(answers);

        return resources;
    }
}