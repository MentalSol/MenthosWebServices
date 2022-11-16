using AutoMapper;
using Menthos.API.Security.Authorization.Attributes;
using Menthos.API.Security.Domain.Models;
using Menthos.API.Security.Domain.Services;
using Menthos.API.Security.Domain.Services.Communication;
using Menthos.API.Security.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Menthos.API.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IMapper _mapper;
    
    public TeacherController(ITeacherService teacherService, IMapper mapper)
    {
        _teacherService = teacherService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _teacherService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _teacherService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var teachers = await _teacherService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherResource>>(teachers);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var teacher = await _teacherService.GetByIdAsync(id);
        var resource = _mapper.Map<Teacher, TeacherResource>(teacher);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _teacherService.UpdateAsync(id, request);
        return Ok(new { message = "Teacher updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _teacherService.DeleteAsync(id);
        return Ok(new { message = "Teacher deleted successfully" });
    }
}