using System.ComponentModel.DataAnnotations;

namespace FFXIVGuide.Web.Models.Instances;

public class CreateModalModel
{
    public Dictionary<int, string> RouletteTypes { get; set; } = new Dictionary<int, string>();

    public string Name { get; set; }

    [Display(Name = "Roulette Type")]
    public int RouletteTypeId { get; set; }
}
