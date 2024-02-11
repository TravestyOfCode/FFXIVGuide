using FFXIVGuide.Web.Data.Note.Commands;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class DeleteNoteAuthorization : IPipelineBehavior<DeleteNote, Result<NoteModel>>
{
    private readonly ILogger<DeleteNoteAuthorization> _logger;

    public DeleteNoteAuthorization(ILogger<DeleteNoteAuthorization> logger)
    {
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(DeleteNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // TODO: User can delete their note, 
            // Admin can delete any note.
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}
