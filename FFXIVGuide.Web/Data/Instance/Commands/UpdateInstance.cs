using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Instance.Commands;

public class UpdateInstance : IRequest<Result<InstanceModel>>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int RouletteTypeId { get; set; }

    internal void MapTo(Entity.Instance entity)
    {
        entity.Name = Name;
        entity.RouletteTypeId = RouletteTypeId;
    }
}

public class UpdateInstanceHandler : IRequestHandler<UpdateInstance, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateInstanceHandler> _logger;

    public UpdateInstanceHandler(ApplicationDBContext dbContext, ILogger<UpdateInstanceHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(UpdateInstance request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Instances
                .AsTracking()
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<InstanceModel>();
            }

            request.MapTo(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}