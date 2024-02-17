using FFXIVGuide.Web.Data.Encounter.Commands;
using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class UpdateEncounterValidation : IPipelineBehavior<UpdateEncounter, Result<EncounterModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateEncounterValidation> _logger;

    public UpdateEncounterValidation(ApplicationDBContext dbContext, ILogger<UpdateEncounterValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(UpdateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check that InstanceId is valid
            if (!await _dbContext.Instances.AnyAsync(p => p.Id.Equals(request.InstanceId), cancellationToken))
            {
                return Result.BadRequest<EncounterModel>(nameof(request.InstanceId), "The InstanceId provided does not exist.");
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}
