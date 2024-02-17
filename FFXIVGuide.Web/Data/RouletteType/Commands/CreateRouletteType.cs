using System.ComponentModel.DataAnnotations;
using FFXIVGuide.Web.Data.Result;

namespace FFXIVGuide.Web.Data.RouletteType.Commands;

public class CreateRouletteType : IRequest<Result<RouletteTypeModel>>
{
    [Required]
    [MaxLength(32)]
    public string Name { get; set; }

    public CreateRouletteType()
    {

    }

    public CreateRouletteType(string name)
    {
        Name = name;
    }

    internal Entity.RouletteType AsEntity() => new Entity.RouletteType() { Name = Name };
}

public class CreateRouletteTypeHandler : IRequestHandler<CreateRouletteType, Result<RouletteTypeModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateRouletteTypeHandler> _logger;

    public CreateRouletteTypeHandler(ApplicationDBContext dbContext, ILogger<CreateRouletteTypeHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<RouletteTypeModel>> Handle(CreateRouletteType request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.RouletteTypes.Add(request.AsEntity());

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Created(entity.Entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<RouletteTypeModel>();
        }
    }
}
