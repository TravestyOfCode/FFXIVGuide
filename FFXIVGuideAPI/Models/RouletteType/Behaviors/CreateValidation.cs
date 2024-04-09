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
/// A PipelineBehavior that validates the CreateRoulette command before handling the request.
/// </summary>
internal class CreateValidation : IPipelineBehavior<CreateRouletteType, Result<RouletteType>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<CreateValidation> _logger;

    public CreateValidation(ApplicationDbContext dbContext, ILogger<CreateValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteType>> Handle(CreateRouletteType request, RequestHandlerDelegate<Result<RouletteType>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check for empty string
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return AppError.BadRequest<RouletteType>("Name", "A Name is required.");
            }

            // Check for max length
            if (request.Name.Length > 64)
            {
                return AppError.BadRequest<RouletteType>("Name", $"Name but be between 1 and 64 characters. The value provided is {request.Name.Length} characters.");
            }

            // Check for duplicate names.
            if (await _dbContext.RouletteTypes.AnyAsync(p => p.Name.Equals(request.Name), cancellationToken))
            {
                return AppError.BadRequest<RouletteType>("Name", "A RouletteType with this name already exists.");
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return AppError.ServerError<RouletteType>();
        }
    }
}
