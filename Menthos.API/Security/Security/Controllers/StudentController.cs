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
public class UsersController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    
    public UsersController(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _studentService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _studentService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        var resource = _mapper.Map<Student, StudentResource>(student);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _studentService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
}