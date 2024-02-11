using FFXIVGuide.Web.Data.Encounter.Commands;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class DeleteEncounterValidation : IPipelineBehavior<DeleteEncounter, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteEncounterValidation> _logger;

    public DeleteEncounterValidation(ApplicationDBContext dbContext, ILogger<DeleteEncounterValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteEncounter request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check if exists, as tracking so the delete handler
            // will not need a db query.
            if (await _dbContext.Encounters.AsTracking().SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken) == null)
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
