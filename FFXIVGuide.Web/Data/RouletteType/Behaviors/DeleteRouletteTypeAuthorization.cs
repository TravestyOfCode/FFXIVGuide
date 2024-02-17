using FFXIVGuide.Web.Data.Result;
using FFXIVGuide.Web.Data.RouletteType.Commands;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class DeleteRouletteTypeAuthorization : IPipelineBehavior<DeleteRouletteType, Result<Unit>>
{
    private readonly ILogger<DeleteRouletteTypeAuthorization> _logger;

    private readonly IUserAccessor _user;

    public DeleteRouletteTypeAuthorization(ILogger<DeleteRouletteTypeAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<Unit>> Handle(DeleteRouletteType request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only an Admin can delete roulette types
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                return Result.Forbidden<Unit>();
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
