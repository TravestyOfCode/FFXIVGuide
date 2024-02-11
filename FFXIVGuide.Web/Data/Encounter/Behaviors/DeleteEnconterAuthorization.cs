using FFXIVGuide.Web.Data.Encounter.Commands;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class DeleteEncounterAuthorization : IPipelineBehavior<DeleteEncounter, Result<EncounterModel>>
{
    private readonly ILogger<DeleteEncounterAuthorization> _logger;

    public DeleteEncounterAuthorization(ILogger<DeleteEncounterAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(DeleteEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Users can delete their own Encounters.
            // Admin can delete any Encounter.
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}
