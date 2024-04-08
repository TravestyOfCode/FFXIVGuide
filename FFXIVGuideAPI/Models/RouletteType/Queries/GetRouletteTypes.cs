using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Queries;

/// <summary>
/// Represents a query to get all of the RouletteTypes.
/// Returns an empty list if no RouletteTypes exist.
/// </summary>
public class GetRouletteTypes : IRequest<Result<List<RouletteType>>>
{

}

internal class GetRouletteTypesHandler : IRequestHandler<GetRouletteTypes, Result<List<RouletteType>>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<GetRouletteTypes> _logger;

    public GetRouletteTypesHandler(ApplicationDbContext dbContext, ILogger<GetRouletteTypes> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<RouletteType>>> Handle(GetRouletteTypes request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.RouletteTypes
                .Select(p => new RouletteType()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return AppError.ServerError<List<RouletteType>>();
        }
    }
}
