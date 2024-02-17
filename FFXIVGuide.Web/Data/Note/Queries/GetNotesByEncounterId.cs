using FFXIVGuide.Web.Data.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FFXIVGuide.Web.Data.Note.Queries;

public class GetNotesByEncounterId : IRequest<Result<List<NoteModel>>>
{
    public int EncounterId { get; set; }

    public string OwnerId { get; set; }
}

public class GetNotesByEncounterIdHandler : IRequestHandler<GetNotesByEncounterId, Result<List<NoteModel>>>
{
    private readonly ApplicationDBContext _dbContext;

    private readonly ILogger<GetNotesByEncounterIdHandler> _logger;

    public GetNotesByEncounterIdHandler(ApplicationDBContext dbContext, ILogger<GetNotesByEncounterIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<List<NoteModel>>> Handle(GetNotesByEncounterId request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _dbContext.Notes
                .Where(p => p.EncounterId.Equals(request.EncounterId))
                .Where(p => p.OwnerId.Equals(request.OwnerId) || p.OwnerId.Equals(null))
                .ProjectToModel()
                .ToListAsync(cancellationToken);

            return Result.Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error.");

            return Result.ServerError<List<NoteModel>>();
        }
    }
}