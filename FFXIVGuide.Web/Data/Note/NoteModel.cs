using FFXIVGuide.Web.Data.Encounter;
using System.Linq;

namespace FFXIVGuide.Web.Data.Note;

public class NoteModel
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public int EncounterId { get; set; }

    public EncounterModel Encounter { get; set; }

    public int Ordinal { get; set; }

    public string Description { get; set; }
}

public static class NoteModelExtensions
{
    public static NoteModel AsModel(this Entity.Note entity)
    {
        return new NoteModel()
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            EncounterId = entity.EncounterId,
            Encounter = entity.Encounter.AsModel(),
            Ordinal = entity.Ordinal,
            Description = entity.Description
        };
    }

    public static IQueryable<NoteModel> ProjectToModel(this IQueryable<Entity.Note> query)
    {
        return query?.Select(p => new NoteModel()
        {
            Id = p.Id,
            OwnerId = p.OwnerId,
            EncounterId = p.EncounterId,
            Encounter = p.Encounter.AsModel(),
            Ordinal = p.Ordinal,
            Description = p.Description
        });
    }
}