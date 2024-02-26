using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Instance.Queries;

public class GetInstancesPaged : PagedRequest<InstanceModel>
{

}

public class GetInstancesPagedHandler : IRequestHandler<GetInstancesPaged, PagedResult<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetInstancesPagedHandler> _logger;

    public GetInstancesPagedHandler(ApplicationDBContext dbContext, ILogger<GetInstancesPagedHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedResult<InstanceModel>> Handle(GetInstancesPaged request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.Instances
                .AsPagedQuery(request)
                .ProjectToModel()
                .ToListAsync(cancellationToken);

            var count = await _dbContext.Instances.CountAsync(cancellationToken);

            return PagedResult<InstanceModel>.Ok(entities, request, count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return PagedResult<InstanceModel>.ServerError(request);
        }
    }
}
