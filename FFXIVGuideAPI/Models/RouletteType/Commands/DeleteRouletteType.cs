using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Commands;

/// <summary>
/// Represents a command to delete a RouletteType.
/// </summary>
public class DeleteRouletteType : IRequest<Result<Unit>>
{
    /// <summary>
    /// The unique identifier of the RouletteType.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="DeleteRouletteType"/>.
    /// </summary>
    public DeleteRouletteType()
    {

    }

    /// <summary>
    /// Creates a new instance of <see cref="DeleteRouletteType"/> with the specified Id.
    /// </summary>
    /// <param name="id">The unique identifier for the RouletteType.</param>
    public DeleteRouletteType(int id)
    {
        Id = id;
    }
}

internal class DeleteRouletteTypeHandler : IRequestHandler<DeleteRouletteType, Result<Unit>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<DeleteRouletteTypeHandler> _logger;

    public DeleteRouletteTypeHandler(ApplicationDbContext dbContext, ILogger<DeleteRouletteTypeHandler> logger)
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
                return AppError.NotFound<Unit>();
            }

            _dbContext.RouletteTypes.Remove(entity);

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
