using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Queries;

/// <summary>
/// Represents a query that gets a single RouletteType by the unique identifier.
/// Returns a NotFound result if the requested RouletteType does not exist.
/// </summary>
public class GetRouletteTypeById : IRequest<Result<RouletteType>>
{
    /// <summary>
    /// The unique identifier of the RouletteType to get.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="GetRouletteTypeById"/>.
    /// </summary>
    public GetRouletteTypeById()
    {

    }

    /// <summary>
    /// Creates a new instance of <see cref="GetRouletteTypeById"/> with the specified Id.
    /// </summary>
    /// <param name="id">The unique identifier for the RouletteType to get.</param>
    public GetRouletteTypeById(int id)
    {
        Id = id;
    }
}

internal class GetRouletteTypeByIdHandler : IRequestHandler<GetRouletteTypeById, Result<RouletteType>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<GetRouletteTypeByIdHandler> _logger;

    public GetRouletteTypeByIdHandler(ApplicationDbContext dbContext, ILogger<GetRouletteTypeByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteType>> Handle(GetRouletteTypeById request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.RouletteTypes
                .Where(p => p.Id.Equals(request.Id))
                .Select(p => new RouletteType()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return AppError.NotFound<RouletteType>();
            }

            return Result.Ok(entity);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.Fail<RouletteType>();
        }
    }
}
