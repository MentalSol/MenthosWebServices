using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class VideoResponse : BaseResponse<Video>
{
    public VideoResponse(string message) : base(message)
    {
    }

    public VideoResponse(Video resource) : base(resource)
    {
    }
}