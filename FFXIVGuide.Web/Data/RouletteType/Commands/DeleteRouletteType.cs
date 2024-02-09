using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType.Commands;

public class DeleteRouletteType : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}

public class DeleteRouletteTypeHandler : IRequestHandler<DeleteRouletteType, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteRouletteTypeHandler> _logger;

    public DeleteRouletteTypeHandler(ApplicationDBContext dbContext, ILogger<DeleteRouletteTypeHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteRouletteType request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.RouletteTypes
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<Unit>();
            }

            _dbContext.RouletteTypes.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok<Unit>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
