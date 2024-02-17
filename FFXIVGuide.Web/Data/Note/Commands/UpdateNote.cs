using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Note.Commands;

public class UpdateNote : IRequest<Result<NoteModel>>
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public int EncounterId { get; set; }

    public int Ordinal { get; set; }

    public string Description { get; set; }

    internal void MapTo(Entity.Note entity)
    {
        entity.OwnerId = OwnerId;
        entity.EncounterId = EncounterId;
        entity.Ordinal = Ordinal;
        entity.Description = Description;
    }
}

public class UpdateNoteHandler : IRequestHandler<UpdateNote, Result<NoteModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateNoteHandler> _logger;

    public UpdateNoteHandler(ApplicationDBContext dbContext, ILogger<UpdateNoteHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(UpdateNote request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Notes
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<NoteModel>();
            }

            request.MapTo(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}