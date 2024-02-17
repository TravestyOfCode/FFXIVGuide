using FFXIVGuide.Web.Data.Result;

namespace FFXIVGuide.Web.Data.Note.Commands;

public class CreateNote : IRequest<Result<NoteModel>>
{
    public string OwnerId { get; set; }

    public int EncounterId { get; set; }

    public int Ordinal { get; set; }

    public string Description { get; set; }

    internal Entity.Note AsEntity() => new Entity.Note()
    {
        OwnerId = OwnerId,
        EncounterId = EncounterId,
        Ordinal = Ordinal,
        Description = Description
    };
}

public class CreateNoteHandler : IRequestHandler<CreateNote, Result<NoteModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<CreateNoteHandler> _logger;

    public CreateNoteHandler(ApplicationDBContext dbContext, ILogger<CreateNoteHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(CreateNote request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _dbContext.Notes.Add(request.AsEntity());

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Created(entity.Entity.AsModel());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}