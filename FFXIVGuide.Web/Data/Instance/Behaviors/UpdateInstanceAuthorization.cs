using FFXIVGuide.Web.Data.Instance.Commands;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class UpdateInstanceAuthorization : IPipelineBehavior<UpdateInstance, Result<InstanceModel>>
{
    private readonly ILogger<UpdateInstanceAuthorization> _logger;

    public UpdateInstanceAuthorization(ILogger<UpdateInstanceAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(UpdateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: User can update their own Instances, Admin can
            // update any instance.
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}
