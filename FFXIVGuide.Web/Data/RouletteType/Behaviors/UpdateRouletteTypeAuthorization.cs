using FFXIVGuide.Web.Data.RouletteType.Commands;
using MediatR;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class UpdateRouletteTypeAuthorization : IPipelineBehavior<UpdateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ILogger<UpdateRouletteTypeAuthorization> _logger;

    public UpdateRouletteTypeAuthorization(ILogger<UpdateRouletteTypeAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(UpdateRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Only an Admin role user will be able to update RouletteTypes
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
