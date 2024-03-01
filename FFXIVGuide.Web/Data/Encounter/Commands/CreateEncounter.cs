namespace FFXIVGuide.Web.Data.Encounter.Commands;

public class CreateEncounter : IRequest<Result<EncounterModel>>
{
    public string OwnerId { get; set; }

    public int InstanceId { get; set; }

    public string Name { get; set; }

    public int Ordinal { get; set; }

    public CreateEncounter()
    {

    }

    public CreateEncounter(int instanceId, string name, string ownerId)
    {
        InstanceId = instanceId;
        Name = name;
        OwnerId = ownerId;
    }

    internal Entity.Encounter AsEntity() => new Entity.Encounter()
    {
        OwnerId = OwnerId,
        InstanceId = InstanceId,
        Name = Name,
        Ordinal = Ordinal
    };

}

public class CreateEncounterHandler : IRequestHandler<CreateEncounter, Result<EncounterModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateEncounterHandler> _logger;

    public CreateEncounterHandler(ApplicationDBContext dbContext, ILogger<CreateEncounterHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<EncounterModel>> Handle(CreateEncounter request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.Encounters.Add(request.AsEntity());

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.Entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<EncounterModel>();
        }
    }
}
