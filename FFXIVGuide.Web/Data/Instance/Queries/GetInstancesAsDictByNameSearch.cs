using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Queries;

public class GetInstancesAsDictByNameSearch : IRequest<Result<Dictionary<int, string>>>
{
    public string Search { get; set; }
}

public class GetInstancesAsDictByNameSearchHandler : IRequestHandler<GetInstancesAsDictByNameSearch, Result<Dictionary<int, string>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetInstancesAsDictByNameSearchHandler> _logger;

    public GetInstancesAsDictByNameSearchHandler(ApplicationDBContext dbContext, ILogger<GetInstancesAsDictByNameSearchHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Dictionary<int, string>>> Handle(GetInstancesAsDictByNameSearch request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.Instances
                .Where(p => p.Name.Contains(request.Search))
                .ToDictionaryAsync(p => p.Id, p => p.Name);

            return Result.Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Dictionary<int, string>>();
        }
    }
}
