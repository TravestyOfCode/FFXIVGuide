using FFXIVGuide.Web.Data.Encounter.Commands;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class CreateEncounterAuthorization : IPipelineBehavior<CreateEncounter, Result<EncounterModel>>
{
    private readonly ILogger<CreateEncounterAuthorization> _logger;

    public CreateEncounterAuthorization(ILogger<CreateEncounterAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(CreateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: User can create encounters if they are the OwnerId
            // Admin can create for any OwnerId
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}
