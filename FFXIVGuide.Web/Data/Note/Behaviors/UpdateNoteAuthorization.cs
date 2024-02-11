using FFXIVGuide.Web.Data.Note.Commands;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class UpdateNoteAuthorization : IPipelineBehavior<UpdateNote, Result<NoteModel>>
{
    private readonly ILogger<UpdateNoteAuthorization> _logger;

    public UpdateNoteAuthorization(ILogger<UpdateNoteAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(UpdateNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: User can update their own notes.
            // Admin can update any note.
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}
