using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType.Queries;

public class GetAllRouletteTypesAsDict : IRequest<Result<Dictionary<int, string>>>
{

}

public class GetAllRouletteTypesAsDictHandler : IRequestHandler<GetAllRouletteTypesAsDict, Result<Dictionary<int, string>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetAllRouletteTypesAsDictHandler> _logger;

    public GetAllRouletteTypesAsDictHandler(ApplicationDBContext dbContext, ILogger<GetAllRouletteTypesAsDictHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Dictionary<int, string>>> Handle(GetAllRouletteTypesAsDict request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.RouletteTypes
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
