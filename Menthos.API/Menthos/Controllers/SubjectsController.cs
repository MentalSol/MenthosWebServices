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
[SwaggerTag("Create, read, update and delete Subjects")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _subjectService;
    private readonly IMapper _mapper;

    public SubjectsController(ISubjectService subjectService, IMapper mapper)
    {
        _subjectService = subjectService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SubjectResource>), 200)]
    public async Task<IEnumerable<SubjectResource>> GetAllAsync()
    {
        var subjects = await _subjectService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectResource>>(subjects);

        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SubjectResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveSubjectResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var subject = _mapper.Map<SaveSubjectResource, Subject>(resource);

        var result = await _subjectService.SaveAsync(subject);

        if (!result.Success)
            return BadRequest(result.Message);

        var subjectResource = _mapper.Map<Subject, SubjectResource>(result.Resource);

        return Created(nameof(PostAsync), subjectResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubjectResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var subject = _mapper.Map<SaveSubjectResource, Subject>(resource);
        var result = await _subjectService.UpdateAsync(id, subject);

        if (!result.Success)
            return BadRequest(result.Message);

        var subjectResource = _mapper.Map<Subject, SubjectResource>(result.Resource);

        return Ok(subjectResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _subjectService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var subjectResource = _mapper.Map<Subject, SubjectResource>(result.Resource);

        return Ok(subjectResource);
    }
}