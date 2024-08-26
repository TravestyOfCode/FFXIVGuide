using FFXIVGuide.Models.Encounter;
using FFXIVGuide.Models.InstanceType;
using FFXIVGuide.Models.RouletteType;

namespace FFXIVGuide.Models.Instance;

public class InstanceModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageLocation { get; set; }

    public required InstanceTypeModel InstanceType { get; set; }

    public IEnumerable<RouletteTypeModel> RouletteTypes { get; set; } = new List<RouletteTypeModel>();

    public IEnumerable<EncounterModel> Encounters { get; set; } = new List<EncounterModel>();
}
