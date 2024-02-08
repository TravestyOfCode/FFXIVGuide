using System.Collections.Generic;

namespace FFXIVGuide.Web.Models.Guide;

public class InstanceDetailModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public IEnumerable<EncounterModel> Encounters { get; set; }
}
