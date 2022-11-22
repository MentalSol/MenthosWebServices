using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/videos/{videoId}/comments")]
public class VideoCommentsController
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    
    public VideoCommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Comments for given Comment",
        Description = "Get existing Comments associated with the specified Video",
        OperationId = "GetVideoComments",
        Tags = new[] { "Videos" }
    )]
    public async Task<IEnumerable<CommentResource>> GetAllByVideoIdAsync(int videoId)
    {
        var comments = await _commentService.ListByVideoIdAsync(videoId);

        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;
    }
}