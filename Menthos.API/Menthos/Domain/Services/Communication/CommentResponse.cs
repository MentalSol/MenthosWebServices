using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Shared.Domain.Services.Communication;

namespace Menthos.API.Menthos.Domain.Services.Communication;

public class CommentResponse : BaseResponse<Comment>
{
    public CommentResponse(string message) : base(message)
    {
    }

    public CommentResponse(Comment resource) : base(resource)
    {
    }
}