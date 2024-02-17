using FFXIVGuide.Web.Data.Note.Commands;
using FFXIVGuide.Web.Services;
using System.Security.Claims;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class CreateNoteAuthorization : IPipelineBehavior<CreateNote, Result<NoteModel>>
{
    private readonly ILogger<CreateNoteAuthorization> _logger;

    private readonly IUserAccessor _user;

    public CreateNoteAuthorization(ILogger<CreateNoteAuthorization> logger, IUserAccessor user)
    {
        _logger = logger;
        _user = user;
    }

    public async Task<Result<NoteModel>> Handle(CreateNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // User can create notes if themselves
            // Admin can create notes for any user
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                var userId = _user.CurrentUser.FindFirstValue(ClaimTypes.NameIdentifier);

                ArgumentNullException.ThrowIfNull(userId);

                if (!request.OwnerId.Equals(userId))
                {
                    return Result.Forbidden<NoteModel>();
                }
            }
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<NoteModel>();
        }
    }
}
