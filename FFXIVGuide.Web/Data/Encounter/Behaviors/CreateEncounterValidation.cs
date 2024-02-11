using FFXIVGuide.Web.Data.Encounter.Commands;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class CreateEncounterValidation : IPipelineBehavior<CreateEncounter, Result<EncounterModel>>
{
    private readonly ILogger<CreateEncounterValidation> _logger;

    public CreateEncounterValidation(ILogger<CreateEncounterValidation> logger)
    {
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(CreateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}
