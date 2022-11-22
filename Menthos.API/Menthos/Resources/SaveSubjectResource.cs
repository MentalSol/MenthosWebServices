using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Menthos.API.Menthos.Resources;

[SwaggerSchema(Required = new []{"Name"})]
public class SaveSubjectResource
{
    [SwaggerSchema("Subject Name")]
    [Required]
    public string Name { get; set; }
    
    [SwaggerSchema("Subject Image")]
    public string Image { get; set; }    
}