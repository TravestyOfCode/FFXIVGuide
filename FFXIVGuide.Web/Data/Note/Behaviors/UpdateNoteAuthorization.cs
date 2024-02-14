using FFXIVGuide.Web.Data.Note.Commands;
using FFXIVGuide.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class UpdateNoteAuthorization : IPipelineBehavior<UpdateNote, Result<NoteModel>>
{
    private readonly ILogger<UpdateNoteAuthorization> _logger;

    private readonly IUserAccessor _user;

    private readonly ApplicationDBContext _dbContext;

    public UpdateNoteAuthorization(ILogger<UpdateNoteAuthorization> logger, IUserAccessor user, ApplicationDBContext dbContext)
    {
        _logger = logger;
        _user = user;
        _dbContext = dbContext;
    }

    public async Task<Result<NoteModel>> Handle(UpdateNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // User can update their own notes.
            // Admin can update any note.
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                var userId = _user.CurrentUser.FindFirstValue(ClaimTypes.NameIdentifier);

                ArgumentNullException.ThrowIfNull(userId);

                if (!await _dbContext.Notes.AnyAsync(p => p.Id.Equals(request.Id) && p.OwnerId.Equals(userId), cancellationToken))
                {
                    return Result.Forbidden<NoteModel>();
                }

                return await next();
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
