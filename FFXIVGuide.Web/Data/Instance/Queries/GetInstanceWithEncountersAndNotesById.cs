using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Queries;

public class GetInstanceWithEncountersAndNotesById : IRequest<Result<InstanceModel>>
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public GetInstanceWithEncountersAndNotesById()
    {

    }

    public GetInstanceWithEncountersAndNotesById(int id, string ownerId)
    {
        Id = id;
        OwnerId = ownerId;
    }
}

public class GetInstanceWithEncountersAndNotesByIdHandler : IRequestHandler<GetInstanceWithEncountersAndNotesById, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetInstanceWithEncountersAndNotesByIdHandler> _logger;

    public GetInstanceWithEncountersAndNotesByIdHandler(ApplicationDBContext dbContext, ILogger<GetInstanceWithEncountersAndNotesByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(GetInstanceWithEncountersAndNotesById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Instances
                .Where(p => p.Id.Equals(request.Id))
                .Include(p => p.Encounters.Where(p => p.OwnerId.Equals(request.OwnerId) || p.OwnerId.Equals(null)))
                .ThenInclude(p => p.Notes)
                .ProjectToModel()
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<InstanceModel>();
            }

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}