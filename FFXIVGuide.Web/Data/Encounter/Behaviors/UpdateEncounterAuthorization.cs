using FFXIVGuide.Web.Data.Encounter.Commands;
using FFXIVGuide.Web.Data.Result;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class UpdateEncounterAuthorization : IPipelineBehavior<UpdateEncounter, Result<EncounterModel>>
{
    private readonly ILogger<UpdateEncounterAuthorization> _logger;

    public UpdateEncounterAuthorization(ILogger<UpdateEncounterAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(UpdateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: User is able to delete their own encounters.
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
