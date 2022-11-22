using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/subjects/{subjectId}/videos")]
public class SubjectVideosController
{
    private readonly IVideoService _videoService;
    private readonly IMapper _mapper;
    
    public SubjectVideosController(IVideoService videoService, IMapper mapper)
    {
        _videoService = videoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Videos for given Video",
        Description = "Get existing Videos associated with the specified Subject",
        OperationId = "GetSubjectVideos",
        Tags = new[] { "Subjects" }
    )]
    public async Task<IEnumerable<VideoResource>> GetAllBySubjectIdAsync(int subjectId)
    {
        var videos = await _videoService.ListBySubjectIdAsync(subjectId);

        var resources = _mapper.Map<IEnumerable<Video>, IEnumerable<VideoResource>>(videos);

        return resources;
    }
}