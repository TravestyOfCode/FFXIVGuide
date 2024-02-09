using FFXIVGuide.Web.Data.RouletteType.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public class CreateRouletteTypeValidation : IPipelineBehavior<CreateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateRouletteTypeValidation> _logger;

    public CreateRouletteTypeValidation(ApplicationDBContext dbContext, ILogger<CreateRouletteTypeValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(CreateRouletteType request, RequestHandlerDelegate<Result<RouletteTypeModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check for duplicate name
            if (await _dbContext.RouletteTypes.AnyAsync(p => p.Name.Equals(request.Name), cancellationToken))
            {
                return Result.BadRequest<RouletteTypeModel>(nameof(request.Name), "A RouletteType with this name already exists.");
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
