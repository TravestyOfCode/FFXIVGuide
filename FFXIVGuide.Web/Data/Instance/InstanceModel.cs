using FFXIVGuide.Web.Data.Encounter;
using FFXIVGuide.Web.Data.RouletteType;
using System.Linq;

namespace FFXIVGuide.Web.Data.Instance;

public class InstanceModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int RouletteTypeId { get; set; }

    public RouletteTypeModel RouletteType { get; set; }

    public string ImageUrl { get; set; }

    public List<EncounterModel> Encounters { get; set; }
}

public static class InstanceModelExtensions
{
    public static InstanceModel AsModel(this Entity.Instance entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new InstanceModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            RouletteTypeId = entity.RouletteTypeId,
            RouletteType = entity.RouletteType.AsModel(),
            ImageUrl = entity.ImageUrl,
            Encounters = entity.Encounters?.Select(p => p.AsModel()).ToList()
        };
    }

    public static IQueryable<InstanceModel> ProjectToModel(this IQueryable<Entity.Instance> query)
    {
        return query?.Select(p => new InstanceModel()
        {
            Id = p.Id,
            Name = p.Name,
            RouletteTypeId = p.RouletteTypeId,
            RouletteType = p.RouletteType.AsModel(),
            ImageUrl = p.ImageUrl
        });
    }
}