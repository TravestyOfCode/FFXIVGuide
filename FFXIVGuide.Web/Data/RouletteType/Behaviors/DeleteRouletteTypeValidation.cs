using FFXIVGuide.Web.Data.RouletteType.Commands;
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
            // Check if exists
            if (!await _dbContext.RouletteTypes.AnyAsync(p => p.Id.Equals(request.Id), cancellationToken))
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
