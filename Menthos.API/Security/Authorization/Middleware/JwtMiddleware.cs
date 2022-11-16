using Menthos.API.Security.Authorization.Handlers.Interfaces;
using Menthos.API.Security.Authorization.Settings;
using Menthos.API.Security.Domain.Services;
using Microsoft.Extensions.Options;

namespace Menthos.API.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IStudentService studentService, ITeacherService teacherService, IJwtHandler handler)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            context.Items["Student"] = await studentService.GetByIdAsync(userId.Value);
            context.Items["Teacher"] = await teacherService.GetByIdAsync(userId.Value);
        }

        await _next(context);
    }
}