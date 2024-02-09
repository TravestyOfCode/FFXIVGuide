using FFXIVGuide.Web.Data.Instance.Commands;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class CreateInstanceValidation : IPipelineBehavior<CreateInstance, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateInstanceValidation> _logger;

    public CreateInstanceValidation(ApplicationDBContext dbContext, ILogger<CreateInstanceValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(CreateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            var result = Result.BadRequest<InstanceModel>();

            // Check for duplicate name
            if (await _dbContext.Instances.AnyAsync(p => p.Name.Equals(request.Name), cancellationToken))
            {
                result.Errors.AddModelError(nameof(request.Name), "A Instance with this name already exists.");
            }

            // Ensure the RouletteTypeId is valid
            if (!await _dbContext.RouletteTypes.AnyAsync(p => p.Id.Equals(request.RouletteTypeId)))
            {
                result.Errors.AddModelError(nameof(request.RouletteTypeId), "The RouletteTypeId provided does not exist.");
            }

            if (result.Errors.Any())
            {
                return result;
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<InstanceModel>();
        }
    }
}
