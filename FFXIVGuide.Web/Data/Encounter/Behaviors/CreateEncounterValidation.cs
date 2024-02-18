using FFXIVGuide.Web.Data.Encounter.Commands;
using FFXIVGuide.Web.Services;
using Microsoft.IdentityModel.Tokens;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class CreateEncounterValidation : IPipelineBehavior<CreateEncounter, Result<EncounterModel>>
{
    private readonly IUserAccessor _user;

    private readonly ILogger<CreateEncounterValidation> _logger;

    public CreateEncounterValidation(ILogger<CreateEncounterValidation> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<EncounterModel>> Handle(CreateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // If the user is an Admin, then OwnerId is not required, nor does it need to be
            // the current user.
            if (!_user.CurrentUser.IsInRole("Admin") && request.OwnerId.IsNullOrEmpty())
            {
                return Result.BadRequest<EncounterModel>();
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
