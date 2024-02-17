using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Data.Result;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class CreateInstanceAuthorization : IPipelineBehavior<CreateInstance, Result<InstanceModel>>
{
    private readonly ILogger<CreateInstanceAuthorization> _logger;

    private readonly IUserAccessor _user;

    public CreateInstanceAuthorization(ILogger<CreateInstanceAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<InstanceModel>> Handle(CreateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only Admin can create new instances.
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                return Result.Forbidden<InstanceModel>();
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}