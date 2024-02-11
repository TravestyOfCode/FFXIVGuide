using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FFXIVGuide.Web.Services;

public class HttpContextUserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public HttpContextUserAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public ClaimsPrincipal CurrentUser => _accessor?.HttpContext?.User;
}
