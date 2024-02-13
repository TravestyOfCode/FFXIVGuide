using FFXIVGuide.Web.Data.Encounter.Commands;
using FFXIVGuide.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class DeleteEncounterAuthorization : IPipelineBehavior<DeleteEncounter, Result<Unit>>
{
    private readonly IUserAccessor _user;

    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteEncounterAuthorization> _logger;

    public DeleteEncounterAuthorization(ILogger<DeleteEncounterAuthorization> logger, IUserAccessor user, ApplicationDBContext dbContext)
    {
        _logger = logger;
        _user = user;
        _dbContext = dbContext;
    }

    public async Task<Result<Unit>> Handle(DeleteEncounter request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Users can delete their own Encounters.
            // Admin can delete any Encounter.            
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                var userId = _user.CurrentUser.FindFirstValue(ClaimTypes.NameIdentifier);

                //if (!await _dbContext.Encounters.AnyAsync(p => p.Id.Equals(request.Id) && p.OwnerId.Equals(userId), cancellationToken))
                if (await _dbContext.Encounters.AsTracking().SingleOrDefaultAsync(p => p.Id.Equals(request.Id) && p.OwnerId.Equals(userId), cancellationToken) == null)
                {
                    return Result.Forbidden<Unit>();
                }
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
