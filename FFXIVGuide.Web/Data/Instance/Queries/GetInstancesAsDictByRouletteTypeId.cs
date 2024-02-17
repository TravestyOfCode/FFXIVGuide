using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Queries;

public class GetInstancesAsDictByRouletteTypeId : IRequest<Result<Dictionary<int, string>>>
{
    public int RouletteTypeId { get; set; }
}

public class GetInstancesAsDictByRouletteTypeIdHandler : IRequestHandler<GetInstancesAsDictByRouletteTypeId, Result<Dictionary<int, string>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetInstancesAsDictByRouletteTypeIdHandler> _logger;

    public GetInstancesAsDictByRouletteTypeIdHandler(ApplicationDBContext dbContext, ILogger<GetInstancesAsDictByRouletteTypeIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Dictionary<int, string>>> Handle(GetInstancesAsDictByRouletteTypeId request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.Instances
                .Where(p => p.RouletteTypeId.Equals(request.RouletteTypeId))
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
