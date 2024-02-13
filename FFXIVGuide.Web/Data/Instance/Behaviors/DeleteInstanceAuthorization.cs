using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class DeleteInstanceAuthorization : IPipelineBehavior<DeleteInstance, Result<InstanceModel>>
{
    private readonly ILogger<DeleteInstanceAuthorization> _logger;

    private readonly IUserAccessor _user;

    public DeleteInstanceAuthorization(ILogger<DeleteInstanceAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<InstanceModel>> Handle(DeleteInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only Admins can delete instances
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
