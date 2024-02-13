using FFXIVGuide.Web.Data.Instance.Commands;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class DeleteInstanceValidation : IPipelineBehavior<DeleteInstance, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteInstanceValidation> _logger;

    public DeleteInstanceValidation(ApplicationDBContext dbContext, ILogger<DeleteInstanceValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteInstance request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check if exists
            if (!await _dbContext.Instances.AnyAsync(p => p.Id.Equals(request.Id), cancellationToken))
            {
                return Result.NotFound<Unit>();
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
