using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType;

public class GetAllRouletteTypes : IRequest<Result<IEnumerable<RouletteTypeModel>>>
{

}

public class GetAllRouletteTypesHandler : IRequestHandler<GetAllRouletteTypes, Result<IEnumerable<RouletteTypeModel>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetAllRouletteTypesHandler> _logger;

    public GetAllRouletteTypesHandler(ApplicationDBContext dbContext, ILogger<GetAllRouletteTypesHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RouletteTypeModel>>> Handle(GetAllRouletteTypes request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.RouletteTypes
                .ProjectToModel()
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<RouletteTypeModel>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<IEnumerable<RouletteTypeModel>>();
        }
    }
}