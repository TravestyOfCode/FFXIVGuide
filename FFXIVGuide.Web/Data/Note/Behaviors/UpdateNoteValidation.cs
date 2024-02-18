using FFXIVGuide.Web.Data.Note.Commands;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Note.Behaviors;

public class UpdateNoteValidation : IPipelineBehavior<UpdateNote, Result<NoteModel>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<UpdateNoteValidation> _logger;

    public UpdateNoteValidation(ApplicationDBContext dbContext, ILogger<UpdateNoteValidation> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<NoteModel>> Handle(UpdateNote request, RequestHandlerDelegate<Result<NoteModel>> next, CancellationToken cancellationToken)
    {
        try
        {
            // Verify the EncounterId is valid
            if (!await _dbContext.Encounters.AnyAsync(p => p.Id.Equals(request.EncounterId), cancellationToken))
            {
                return Result.BadRequest<NoteModel>(nameof(request.EncounterId), "The EncounterId provided does not exist.");
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
