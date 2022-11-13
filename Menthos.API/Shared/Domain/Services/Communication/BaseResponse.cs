namespace Menthos.API.Shared.Domain.Services.Communication;

public abstract class BaseResponse<Q>
{
    protected BaseResponse(string message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }

    protected BaseResponse(Q resource)
    {
        Success = true;
        Message = string.Empty;
        Resource = resource;
    }
    
    public bool Success { get; set; }
    public string Message { get; set; }
    public Q Resource { get; set; }
}