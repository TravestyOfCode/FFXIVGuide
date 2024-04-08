using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Commands;

/// <summary>
/// Represents a command to update an existing RouletteType.
/// </summary>
public class UpdateRouletteType : IRequest<Result<Unit>>
{
    /// <summary>
    /// The unique identifier of the RouletteType.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the RouletteType.
    /// Can not be null or empty and must be unique.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="UpdateRouletteType"/> with an empty Name.
    /// </summary>
    public UpdateRouletteType()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Creates a new instance of <see cref="UpdateRouletteType"/> with the specified Id and Name.
    /// </summary>
    /// <param name="id">The unique identifier of the RouletteType.</param>
    /// <param name="name">The updated name of the RouletteType.
    /// Can not be null or empty and must be unique.
    /// </param>
    public UpdateRouletteType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

internal class UpdateRouletteTypeHandler : IRequestHandler<UpdateRouletteType, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<UpdateRouletteTypeHandler> _logger;

    public UpdateRouletteTypeHandler(ApplicationDbContext dbContext, ILogger<UpdateRouletteTypeHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateRouletteType request, CancellationToken cancellationToken)
    {
        try
        {
            // Get the entity as tracking as tracking is turned off by default.
            var entity = await _dbContext.RouletteTypes
                .AsTracking()
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id));

            if (entity == null)
            {
                return AppError.NotFound<Unit>();
            }

            entity.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return AppError.ServerError<Unit>();
        }
    }
}
