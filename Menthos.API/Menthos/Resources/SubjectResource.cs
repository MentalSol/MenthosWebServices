using Swashbuckle.AspNetCore.Annotations;

namespace Menthos.API.Menthos.Resources;

public class SubjectResource
{
    [SwaggerSchema("Subject Identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Subject Name")]
    public string Name { get; set; }
    [SwaggerSchema("Subject Image")]
    public string Image { get; set; }
}