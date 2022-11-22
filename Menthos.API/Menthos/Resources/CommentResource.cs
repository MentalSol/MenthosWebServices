using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Security.Resources;

namespace Menthos.API.Menthos.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public string Content { get; set; }
    public StudentResource Student { get; set; }
    public VideoResource Video { get; set; }
}