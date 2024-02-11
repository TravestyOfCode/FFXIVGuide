using FFXIVGuide.Web.Data.Note.Commands;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class DeleteNoteValidation : IPipelineBehavior<DeleteNote, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteNoteValidation> _logger;

    public DeleteNoteValidation(ApplicationDBContext dbContext, ILogger<DeleteNoteValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteNote request, RequestHandlerDelegate<Result<Unit>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Check if exists, as tracking so the delete handler
            // will not need a db query.
            if (await _dbContext.Notes.AsTracking().SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken) == null)
            {
                return Result.NotFound<Unit>();
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
