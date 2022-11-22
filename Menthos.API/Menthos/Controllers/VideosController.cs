using AutoMapper;
using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Menthos.Domain.Services;
using Menthos.API.Menthos.Resources;
using Menthos.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Menthos.API.Menthos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class VideosController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IMapper _mapper;

    public VideosController(IVideoService videoService, IMapper mapper)
    {
        _videoService = videoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<VideoResource>> GetAllAsync()
    {
        var videos = await _videoService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Video>, IEnumerable<VideoResource>>(videos);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveVideoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var video = _mapper.Map<SaveVideoResource, Video>(resource);

        var result = await _videoService.SaveAsync(video);

        if (!result.Success)
            return BadRequest(result.Message);

        var videoResource = _mapper.Map<Video, VideoResource>(result.Resource);

        return Ok(videoResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveVideoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var video = _mapper.Map<SaveVideoResource, Video>(resource);

        var result = await _videoService.UpdateAsync(id, video);

        if (!result.Success)
            return BadRequest(result.Message);

        var videoResource = _mapper.Map<Video, VideoResource>(result.Resource);

        return Ok(videoResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _videoService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var videoResource = _mapper.Map<Video, VideoResource>(result.Resource);

        return Ok(videoResource);
    }
}