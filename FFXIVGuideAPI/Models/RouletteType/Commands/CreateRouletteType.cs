using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Commands;

public class CreateRouletteType : IRequest<Result<RouletteType>>
{
    /// <summary>
    /// The display name of the RouletteType. Must be unique.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    public CreateRouletteType()
    {
        Name = string.Empty;
    }

    public CreateRouletteType(string name)
    {
        Name = name;
    }
}

internal class CreateRouletteTypeHandler : IRequestHandler<CreateRouletteType, Result<RouletteType>>
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<CreateRouletteTypeHandler> _logger;

    public CreateRouletteTypeHandler(ApplicationDbContext dbContext, ILogger<CreateRouletteTypeHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteType>> Handle(CreateRouletteType request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.RouletteTypes.Add(new Data.RouletteType()
            {
                Name = request.Name
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.Entity.AsModel()!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return AppError.ServerError<RouletteType>();
        }
    }
}
