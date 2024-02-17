using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;

namespace FFXIVGuide.Web.Data.Note.Commands;

public class DeleteNote : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}

public class DeleteNoteHandler : IRequestHandler<DeleteNote, Result<Unit>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<DeleteNoteHandler> _logger;

    public DeleteNoteHandler(ApplicationDBContext dbContext, ILogger<DeleteNoteHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteNote request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _dbContext.Notes
                .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                return Result.NotFound<Unit>();
            }

            _dbContext.Notes.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok<Unit>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<Unit>();
        }
    }
}
