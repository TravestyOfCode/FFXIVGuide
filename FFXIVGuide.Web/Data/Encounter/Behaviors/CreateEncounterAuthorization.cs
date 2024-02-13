using FFXIVGuide.Web.Data.Encounter.Commands;
using FFXIVGuide.Web.Services;
using System.Security.Claims;

namespace FFXIVGuide.Web.Data.Encounter.Behaviors;

public class CreateEncounterAuthorization : IPipelineBehavior<CreateEncounter, Result<EncounterModel>>
{
    private readonly IUserAccessor _user;

    private readonly ILogger<CreateEncounterAuthorization> _logger;

    public CreateEncounterAuthorization(ILogger<CreateEncounterAuthorization> logger, IUserAccessor userAccessor)
    {
        _logger = logger;
        _user = userAccessor;
    }

    public async Task<Result<EncounterModel>> Handle(CreateEncounter request, RequestHandlerDelegate<Result<EncounterModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // User can create encounters if they are the OwnerId
            // Admin can create for any OwnerId
            if (!_user.CurrentUser.IsInRole("Admin") && !request.OwnerId.Equals(_user.CurrentUser.FindFirstValue(ClaimTypes.NameIdentifier)))
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
