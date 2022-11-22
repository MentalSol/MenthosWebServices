using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/teachers/{teacherId}/videos")]
public class TeacherVideosController
{
    private readonly IVideoService _videoService;
    private readonly IMapper _mapper;
    
    public TeacherVideosController(IVideoService videoService, IMapper mapper)
    {
        _videoService = videoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Videos for given Video",
        Description = "Get existing Videos associated with the specified Teacher",
        OperationId = "GetTeacherVideos",
        Tags = new[] { "Teacher" }
    )]
    public async Task<IEnumerable<VideoResource>> GetAllByTeacherIdAsync(int teacherId)
    {
        var videos = await _videoService.ListByTeacherIdAsync(teacherId);

        var resources = _mapper.Map<IEnumerable<Video>, IEnumerable<VideoResource>>(videos);

        return resources;
    }
}