using FFXIVGuide.Web.Data.Instance.Commands;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class CreateInstanceAuthorization : IPipelineBehavior<CreateInstance, Result<InstanceModel>>
{
    private readonly ILogger<CreateInstanceAuthorization> _logger;

    public CreateInstanceAuthorization(ILogger<CreateInstanceAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(CreateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Only an Admin role user will be able to create new Instances
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}