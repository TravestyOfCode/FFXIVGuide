using FFXIVGuide.Web.Data.Note.Commands;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class CreateNoteAuthorization : IPipelineBehavior<CreateNote, Result<NoteModel>>
{
    private readonly ILogger<CreateNoteAuthorization> _logger;

    public CreateNoteAuthorization(ILogger<CreateNoteAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(CreateNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: Only an Admin role user will be able to create new Notes
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}
