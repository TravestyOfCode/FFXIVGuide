using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Data.Result;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class UpdateInstanceAuthorization : IPipelineBehavior<UpdateInstance, Result<InstanceModel>>
{
    private readonly ILogger<UpdateInstanceAuthorization> _logger;

    private readonly IUserAccessor _user;

    public UpdateInstanceAuthorization(ILogger<UpdateInstanceAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<InstanceModel>> Handle(UpdateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only Admin can update Instance
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
