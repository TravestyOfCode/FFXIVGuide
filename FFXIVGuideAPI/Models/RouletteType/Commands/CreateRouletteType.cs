using FFXIVGuideAPI.Data;
using FFXIVGuideAPI.Data.Errors;
using LightResults;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FFXIVGuideAPI.Models.RouletteType.Commands;

/// <summary>
/// Represents a command to create a new RouletteType.
/// </summary>
public class CreateRouletteType : IRequest<Result<RouletteType>>
{
    /// <summary>
    /// The name of the RouletteType.
    /// Can not be null or empty and must be unique.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="CreateRouletteType"/> with an empty Name.
    /// Name is set to string.Empty
    /// </summary>
    public CreateRouletteType()
    {
        Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="CreateRouletteType"/> with the specified Name.
    /// </summary>
    /// <param name="name">The Name of the RouletteType.
    /// Can not be null or empty and must be unique.</param>
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
