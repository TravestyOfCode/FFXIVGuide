using FFXIVGuide.Web.Data.RouletteType.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class DeleteRouletteTypeValidation : IPipelineBehavior<DeleteRouletteType, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteRouletteTypeValidation> _logger;

    public DeleteRouletteTypeValidation(ApplicationDBContext dbContext, ILogger<DeleteRouletteTypeValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteRouletteType request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check if exists, as tracking so the delete handler
            // will not need a db query.
            if (await _dbContext.RouletteTypes.AsTracking().SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken) == null)
            {
                return Result.NotFound<Unit>();
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
