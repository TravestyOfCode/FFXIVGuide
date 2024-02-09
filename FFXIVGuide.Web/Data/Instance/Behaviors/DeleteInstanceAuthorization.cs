using FFXIVGuide.Web.Data.Instance.Commands;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class DeleteInstanceAuthorization : IPipelineBehavior<DeleteInstance, Result<InstanceModel>>
{
    private readonly ILogger<DeleteInstanceAuthorization> _logger;

    public DeleteInstanceAuthorization(ILogger<DeleteInstanceAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(DeleteInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Owner can delete their instances, Admin can delete
            // non-owned instances.
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}
