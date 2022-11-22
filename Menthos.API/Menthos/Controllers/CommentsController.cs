using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Menthos.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CommentResource>> GetAllAsync()
    {
        var comments = await _commentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);

        return resources;

    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);

        var result = await _commentService.SaveAsync(comment);

        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);

        var result = await _commentService.UpdateAsync(id, comment);

        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _commentService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);

        return Ok(commentResource);
    }
}