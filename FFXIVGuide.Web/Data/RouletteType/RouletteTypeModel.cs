using FFXIVGuide.Web.Data.Instance;
using System.Linq;

namespace FFXIVGuide.Web.Data.RouletteType;

public class RouletteTypeModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<InstanceModel> Instances { get; set; }
}

public static class RouletteTypeModelExtensions
{
    public static RouletteTypeModel AsModel(this Entity.RouletteType entity)
    {
        return new RouletteTypeModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Instances = entity.Instances.Select(i => i.AsModel())
        };
    }

    public static IQueryable<RouletteTypeModel> ProjectToModel(this IQueryable<Entity.RouletteType> query)
    {
        return query?.Select(p => new RouletteTypeModel()
        {
            Id = p.Id,
            Name = p.Name,
            Instances = p.Instances.Select(p => p.AsModel())
        });
    }
}