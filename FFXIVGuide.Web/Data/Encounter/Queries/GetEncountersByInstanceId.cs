using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Encounter.Queries;

public class GetEncountersByInstanceId : IRequest<Result<List<EncounterModel>>>
{
    public int InstanceId { get; set; }

    public string OwnerId { get; set; }
}

public class GetEncountersByInstanceIdHandler : IRequestHandler<GetEncountersByInstanceId, Result<List<EncounterModel>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetEncountersByInstanceIdHandler> _logger;

    public GetEncountersByInstanceIdHandler(ApplicationDBContext dbContext, ILogger<GetEncountersByInstanceIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<EncounterModel>>> Handle(GetEncountersByInstanceId request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.Encounters
                .Where(p => p.InstanceId.Equals(request.InstanceId))
                .Where(p => p.OwnerId.Equals(request.OwnerId) || p.OwnerId.Equals(null))
                .ProjectToModel()
                .ToListAsync(cancellationToken);

            return Result.Ok(entities);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<List<EncounterModel>>();
        }
    }
}