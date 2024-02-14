using System.Security.Claims;

namespace FFXIVGuide.Web.Services;

public interface IUserAccessor
{
    public ClaimsPrincipal CurrentUser { get; }
}
