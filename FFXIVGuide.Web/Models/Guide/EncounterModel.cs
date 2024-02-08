using System.Collections.Generic;

namespace FFXIVGuide.Web.Models.Guide;
public class EncounterModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<string> Notes { get; set; }
}
