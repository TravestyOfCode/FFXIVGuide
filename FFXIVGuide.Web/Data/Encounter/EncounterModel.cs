using FFXIVGuide.Web.Data.Instance;
using FFXIVGuide.Web.Data.Note;
using System.Linq;

namespace FFXIVGuide.Web.Data.Encounter;

public class EncounterModel
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public int InstanceId { get; set; }

    public InstanceModel Instance { get; set; }

    public string Name { get; set; }

    public int Ordinal { get; set; }

    public IEnumerable<NoteModel> Notes { get; set; }
}

public static class EncounterModelExtensions
{
    public static EncounterModel AsModel(this Entity.Encounter entity)
    {
        return new EncounterModel()
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            InstanceId = entity.InstanceId,
            Instance = entity.Instance.AsModel(),
            Name = entity.Name,
            Ordinal = entity.Ordinal,
            Notes = entity.Notes.Select(p => p.AsModel())
        };
    }

    public static IQueryable<EncounterModel> ProjectToModel(this IQueryable<Entity.Encounter> query)
    {
        return query?.Select(p => new EncounterModel()
        {
            Id = p.Id,
            Name = p.Name,
            OwnerId = p.OwnerId,
            InstanceId = p.InstanceId,
            Instance = p.Instance.AsModel(),
            Ordinal = p.Ordinal,
            Notes = p.Notes.Select(n => n.AsModel())
        });
    }
}