using FFXIVGuide.Web.Data.Instance.Commands;
using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance.Behaviors;

public class UpdateInstanceValidation : IPipelineBehavior<UpdateInstance, Result<InstanceModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateInstanceValidation> _logger;

    public UpdateInstanceValidation(ApplicationDBContext dbContext, ILogger<UpdateInstanceValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<InstanceModel>> Handle(UpdateInstance request, RequestHandlerDelegate<Result<InstanceModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            var result = Result.BadRequest<InstanceModel>();

            // Check for duplicate name
            if (await _dbContext.Instances.AnyAsync(p => p.Name.Equals(request.Name) && p.Id != request.Id, cancellationToken))
            {
                result.Errors.AddModelError(nameof(request.Name), "A Instance with this name already exists.");
            }

            // Check if the RouletteType exists
            if (!await _dbContext.RouletteTypes.AnyAsync(p => p.Id.Equals(request.RouletteTypeId), cancellationToken))
            {
                result.Errors.AddModelError(nameof(request.RouletteTypeId), "The RouletteTypeId specified does not exist.");
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
