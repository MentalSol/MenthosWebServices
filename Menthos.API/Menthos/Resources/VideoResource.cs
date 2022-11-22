using Menthos.API.Menthos.Domain.Models;
using Menthos.API.Security.Resources;

namespace Menthos.API.Menthos.Resources;

public class VideoResource
{
    public int Id { get; set; }
    public string Link { get; set; }
    public TeacherResource Teacher { get; set; }
    public SubjectResource Subject { get; set; } 
}