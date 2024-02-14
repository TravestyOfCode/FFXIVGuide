using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Services;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class DeleteInstanceAuthorization : IPipelineBehavior<DeleteInstance, Result<Unit>>
{
    private readonly ILogger<DeleteInstanceAuthorization> _logger;

    private readonly IUserAccessor _user;

    public DeleteInstanceAuthorization(ILogger<DeleteInstanceAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<Unit>> Handle(DeleteInstance request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Only Admins can delete instances
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                return Result.Forbidden<Unit>();
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
