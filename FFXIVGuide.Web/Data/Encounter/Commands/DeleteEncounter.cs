using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Encounter.Commands;

public class DeleteEncounter : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}

public class DeleteEncounterHandler : IRequestHandler<DeleteEncounter, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteEncounterHandler> _logger;

    public DeleteEncounterHandler(ApplicationDBContext dbContext, ILogger<DeleteEncounterHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteEncounter request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Encounters
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<Unit>();
            }

            _dbContext.Encounters.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok<Unit>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
