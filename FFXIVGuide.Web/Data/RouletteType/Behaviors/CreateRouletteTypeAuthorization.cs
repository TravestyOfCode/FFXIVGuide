using FFXIVGuide.Web.Data.RouletteType.Commands;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class CreateRouletteTypeAuthorization : IPipelineBehavior<CreateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ILogger<CreateRouletteTypeAuthorization> _logger;

    private readonly IUserAccessor _user;

    public CreateRouletteTypeAuthorization(ILogger<CreateRouletteTypeAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<RouletteTypeModel>> Handle(CreateRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only an Admin role user will be able to create new RouletteTypes
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
