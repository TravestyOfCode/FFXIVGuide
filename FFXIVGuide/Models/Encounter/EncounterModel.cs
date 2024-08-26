using FFXIVGuide.Models.EncounterNote;

namespace FFXIVGuide.Models.Encounter;

public class EncounterModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int Ordinal { get; set; }

    public IEnumerable<EncounterNoteModel> EncounterNotes { get; set; } = new List<EncounterNoteModel>();
}
