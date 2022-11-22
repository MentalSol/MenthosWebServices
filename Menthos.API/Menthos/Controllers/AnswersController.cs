using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Menthos.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly IMapper _mapper;

    public AnswersController(IAnswerService answerService, IMapper mapper)
    {
        _answerService = answerService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<AnswerResource>> GetAllAsync()
    {
        var answers = await _answerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerResource>>(answers);

        return resources;
        
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveAnswerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var answer = _mapper.Map<SaveAnswerResource, Answer>(resource);

        var result = await _answerService.SaveAsync(answer);

        if (!result.Success)
            return BadRequest(result.Message);

        var answerResource = _mapper.Map<Answer, AnswerResource>(result.Resource);

        return Ok(answerResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAnswerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var answer = _mapper.Map<SaveAnswerResource, Answer>(resource);

        var result = await _answerService.UpdateAsync(id, answer);

        if (!result.Success)
            return BadRequest(result.Message);

        var answerResource = _mapper.Map<Answer, AnswerResource>(result.Resource);

        return Ok(answerResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _answerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var answerResource = _mapper.Map<Answer, AnswerResource>(result.Resource);

        return Ok(answerResource);
    }
}