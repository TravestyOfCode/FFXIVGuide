namespace FFXIVGuide.Web.Data.Instance.Commands;

public class CreateInstance : IRequest<Result<InstanceModel>>
{
    public string Name { get; set; }

    public int RouletteTypeId { get; set; }

    internal Entity.Instance AsEntity()
    {
        return new Entity.Instance()
        {
            Name = Name,
            RouletteTypeId = RouletteTypeId
        };
    }
}

internal class CreateInstanceHandler : IRequestHandler<CreateInstance, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateInstanceHandler> _logger;

    public CreateInstanceHandler(ApplicationDBContext dbContext, ILogger<CreateInstanceHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(CreateInstance request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.Instances.Add(request.AsEntity());

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.Entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}
