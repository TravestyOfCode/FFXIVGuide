using FFXIVGuide.Web.Data.Note.Commands;
using FFXIVGuide.Web.Data.Result;
using FFXIVGuide.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class DeleteNoteAuthorization : IPipelineBehavior<DeleteNote, Result<Unit>>
{
    private readonly ILogger<DeleteNoteAuthorization> _logger;

    private readonly IUserAccessor _user;

    private readonly ApplicationDBContext _dbContext;

    public DeleteNoteAuthorization(ILogger<DeleteNoteAuthorization> logger, IUserAccessor user, ApplicationDBContext dbContext)
    {
        _logger = logger;
        _user = user;
        _dbContext = dbContext;
    }

    public async Task<Result<Unit>> Handle(DeleteNote request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // User can delete their note, 
            // Admin can delete any note.
            if (!_user.CurrentUser.IsInRole("Admin"))
            {
                var userId = _user.CurrentUser.FindFirstValue(ClaimTypes.NameIdentifier);

                ArgumentNullException.ThrowIfNull(userId);

                if (!await _dbContext.Notes.AnyAsync(p => p.Id.Equals(request.Id) && p.OwnerId.Equals(userId), cancellationToken))
                {
                    return Result.Forbidden<Unit>();
                }
            }

            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
