using System.Security.Claims;

namespace Todo.Api.Authentication;

public class HttpContextUserProfileAccessor : IUserProfileAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextUserProfileAccessor(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;
    
    public string? Subject =>
        _httpContextAccessor.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            ?.Value;
}