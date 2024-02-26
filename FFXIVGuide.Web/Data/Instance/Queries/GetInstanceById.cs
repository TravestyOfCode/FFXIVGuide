using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Queries;

public class GetInstanceById : IRequest<Result<InstanceModel>>
{
    public int Id { get; set; }

    public GetInstanceById()
    {

    }

    public GetInstanceById(int id)
    {
        Id = id;
    }
}

public class GetInstanceByIdHandler : IRequestHandler<GetInstanceById, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetInstanceByIdHandler> _logger;

    public GetInstanceByIdHandler(ApplicationDBContext dbContext, ILogger<GetInstanceByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(GetInstanceById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Instances
                .Where(p => p.Id.Equals(request.Id))
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
