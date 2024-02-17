using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            // All validation/authorization behaviors should have been processed,
            // so no need to query/validate before.
            await _dbContext.Encounters.Where(p => p.Id.Equals(request.Id)).ExecuteDeleteAsync(cancellationToken);

            return Result.Ok<Unit>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
