using FFXIVGuide.Web.Data.RouletteType.Commands;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class UpdateRouletteTypeAuthorization : IPipelineBehavior<UpdateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ILogger<UpdateRouletteTypeAuthorization> _logger;

    private readonly IUserAccessor _user;

    public UpdateRouletteTypeAuthorization(ILogger<UpdateRouletteTypeAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<RouletteTypeModel>> Handle(UpdateRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only an Admin can update RouletteTypes
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                return Result.Forbidden<RouletteTypeModel>();
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
