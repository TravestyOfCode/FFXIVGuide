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
}

public static class InstanceModelExtensions
{
    public static InstanceModel AsModel(this Entity.Instance entity)
    {
        return new InstanceModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            RouletteTypeId = entity.RouletteTypeId,
            RouletteType = entity.RouletteType.AsModel(),
            ImageUrl = entity.ImageUrl
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