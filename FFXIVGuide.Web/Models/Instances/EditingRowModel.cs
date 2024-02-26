using FFXIVGuide.Web.Data.Instance;
using System.ComponentModel.DataAnnotations;

namespace FFXIVGuide.Web.Models.Instances;

public class EditingRowModel
{
    public Dictionary<int, string> RouletteTypes { get; set; } = new Dictionary<int, string>();

    public int Id { get; set; }

    public string Name { get; set; }

    [Display(Name = "Roulette Type")]
    public int RouletteTypeId { get; set; }

    public EditingRowModel()
    {

    }

    public EditingRowModel(InstanceModel instance, Dictionary<int, string> rouletteTypes)
    {
        Id = instance.Id;
        Name = instance.Name;
        RouletteTypeId = instance.RouletteTypeId;
        RouletteTypes = rouletteTypes;
    }
}
