using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Instance.Commands;

public class DeleteInstance : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}

public class DeleteInstanceHandler : IRequestHandler<DeleteInstance, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteInstanceHandler> _logger;

    public DeleteInstanceHandler(ApplicationDBContext dbContext, ILogger<DeleteInstanceHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteInstance request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Instances
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<Unit>();
            }

            _dbContext.Instances.Remove(entity);

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
