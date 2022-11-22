using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/students/{studentId}/comments")]
public class StudentCommentsController
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    
    public StudentCommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments for given Comment",
        Description = "Get existing Comments associated with the specified Student",
        OperationId = "GetStudentComments",
        Tags = new[] { "Student" }
    )]
    public async Task<IEnumerable<CommentResource>> GetAllByStudentIdAsync(int studentId)
    {
        var comments = await _commentService.ListByStudentIdAsync(studentId);

        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;
    }
}