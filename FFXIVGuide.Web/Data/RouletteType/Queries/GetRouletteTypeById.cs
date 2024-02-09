using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.RouletteType.Queries;

public class GetRouletteTypeById : IRequest<Result<RouletteTypeModel>>
{
    public int Id { get; set; }
}

public class GetRouletteTypeByIdHandler : IRequestHandler<GetRouletteTypeById, Result<RouletteTypeModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetRouletteTypeByIdHandler> _logger;

    public GetRouletteTypeByIdHandler(ApplicationDBContext dbContext, ILogger<GetRouletteTypeByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(GetRouletteTypeById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.RouletteTypes
                .Where(p => p.Id.Equals(request.Id))
                .Include(p => p.Instances)
                .ProjectToModel()
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<RouletteTypeModel>();
            }

            return Result.Ok(entity);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
