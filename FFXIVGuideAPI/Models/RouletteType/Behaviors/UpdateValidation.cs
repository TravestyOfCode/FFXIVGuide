using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using FFXIVGuideAPI.Models.RouletteType.Commands;
using LightResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Behaviors;

/// <summary>
/// A PipelineBehavior that validates the UpdateRouletteType command before handling.
/// </summary>
internal class UpdateValidation : IPipelineBehavior<UpdateRouletteType, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<UpdateValidation> _logger;

    public UpdateValidation(ApplicationDbContext dbContext, ILogger<UpdateValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateRouletteType request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check for empty string
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return AppError.BadRequest<Unit>("Name", "A Name is required.");
            }

            // Check for max length
            if (request.Name.Length > 64)
            {
                return AppError.BadRequest<Unit>("Name", $"Name but be between 1 and 64 characters. The value provided is {request.Name.Length} characters.");
            }

            // Check for duplicate name that isn't this specific RouletteId
            if (await _dbContext.RouletteTypes
                .AnyAsync(p => p.Name.Equals(request.Name) && !p.Id.Equals(request.Id), cancellationToken))
            {
                return AppError.BadRequest<Unit>("Name", "A RouletteType with this name already exists.");
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return AppError.ServerError<Unit>();
        }
    }
}
