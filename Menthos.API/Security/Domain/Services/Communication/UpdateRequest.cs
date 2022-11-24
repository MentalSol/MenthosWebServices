namespace Menthos.API.Security.Domain.Services.Communication;

public class UpdateRequest
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Codigo { get; set; }
    public string email { get; set; }
    public int telephone { get; set; }
    public string Password { get; set; }
}