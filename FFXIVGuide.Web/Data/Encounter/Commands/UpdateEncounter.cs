using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Encounter.Commands;

public class UpdateEncounter : IRequest<Result<EncounterModel>>
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public int InstanceId { get; set; }

    public string Name { get; set; }

    public int Ordinal { get; set; }

    internal void MapTo(Entity.Encounter entity)
    {
        entity.OwnerId = OwnerId;
        entity.InstanceId = InstanceId;
        entity.Name = Name;
        entity.Ordinal = Ordinal;
    }
}

public class UpdateEncounterHandler : IRequestHandler<UpdateEncounter, Result<EncounterModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateEncounterHandler> _logger;

    public UpdateEncounterHandler(ApplicationDBContext dbContext, ILogger<UpdateEncounterHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(UpdateEncounter request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Encounters
                .AsTracking()
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<EncounterModel>();
            }

            request.MapTo(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}