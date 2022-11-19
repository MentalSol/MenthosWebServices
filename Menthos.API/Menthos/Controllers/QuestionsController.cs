using System.Net.Mime;
using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Menthos.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Questions")]

public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;

    public QuestionsController(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuestionResource>), 200)]
    public async Task<IEnumerable<QuestionResource>> GetAllAsync()
    {
        var questions = await _questionService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionResource>>(questions);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(QuestionResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveQuestionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var question = _mapper.Map<SaveQuestionResource, Question>(resource);

        var result = await _questionService.SaveAsync(question);

        if (!result.Success)
            return BadRequest(result.Message);

        var questionResource = _mapper.Map<Question, QuestionResource>(result.Resource);

        return Created(nameof(PostAsync), questionResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveQuestionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var question = _mapper.Map<SaveQuestionResource, Question>(resource);
        var result = await _questionService.UpdateAsync(id, question);

        if (!result.Success)
            return BadRequest(result.Message);

        var questionResource = _mapper.Map<Question, QuestionResource>(result.Resource);

        return Ok(questionResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _questionService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var questionResource = _mapper.Map<Question, QuestionResource>(result.Resource);

        return Ok(questionResource);
    }
}