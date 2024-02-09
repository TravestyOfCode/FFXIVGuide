using FFXIVGuide.Web.Data.RouletteType.Commands;
using MediatR;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class DeleteRouletteTypeAuthorization : IPipelineBehavior<DeleteRouletteType, Result<RouletteTypeModel>>
{
    private readonly ILogger<DeleteRouletteTypeAuthorization> _logger;

    public DeleteRouletteTypeAuthorization(ILogger<DeleteRouletteTypeAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(DeleteRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Only an Admin role user will be able to delete RouletteTypes
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
