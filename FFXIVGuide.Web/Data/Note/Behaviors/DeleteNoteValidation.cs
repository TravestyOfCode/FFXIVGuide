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
            // Check if exists
            if (!await _dbContext.Notes.AnyAsync(p => p.Id.Equals(request.Id), cancellationToken))
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
