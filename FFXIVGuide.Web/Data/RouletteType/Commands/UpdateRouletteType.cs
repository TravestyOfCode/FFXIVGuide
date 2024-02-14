using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType.Commands;

public class UpdateRouletteType : IRequest<Result<RouletteTypeModel>>
{
    public int Id { get; set; }

    public string Name { get; set; }

    internal void MapTo(Entity.RouletteType entity)
    {
        entity.Name = Name;
    }
}

public class UpdateRouletteTypeHandler : IRequestHandler<UpdateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateRouletteTypeHandler> _logger;

    public UpdateRouletteTypeHandler(ApplicationDBContext dbContext, ILogger<UpdateRouletteTypeHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(UpdateRouletteType request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.RouletteTypes
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<RouletteTypeModel>();
            }

            request.MapTo(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
