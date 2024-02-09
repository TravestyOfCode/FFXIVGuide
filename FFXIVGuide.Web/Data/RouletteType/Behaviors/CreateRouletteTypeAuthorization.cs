using FFXIVGuide.Web.Data.RouletteType.Commands;
using MediatR;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class CreateRouletteTypeAuthorization : IPipelineBehavior<CreateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ILogger<CreateRouletteTypeAuthorization> _logger;

    public CreateRouletteTypeAuthorization(ILogger<CreateRouletteTypeAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(CreateRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Only an Admin role user will be able to create new RouletteTypes
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
