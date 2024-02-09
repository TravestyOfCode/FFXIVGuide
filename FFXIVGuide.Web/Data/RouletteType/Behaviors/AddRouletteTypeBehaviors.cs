using Microsoft.Extensions.DependencyInjection;

namespace FFXIVGuide.Web.Data.RouletteType.Behaviors;

public static class RouletteTypeBehaviorsExtensions
{
    public static MediatRServiceConfiguration AddRouletteTypeBehaviors(this MediatRServiceConfiguration config)
    {
        // Create Behaviors
        config.AddBehavior<CreateRouletteTypeAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<CreateRouletteTypeValidation>(ServiceLifetime.Scoped);

        // Update Behaviors
        config.AddBehavior<UpdateRouletteTypeAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<UpdateRouletteTypeAuthorization>(ServiceLifetime.Scoped);

        // Delete Behaviors
        config.AddBehavior<DeleteRouletteTypeAuthorization>(ServiceLifetime.Scoped);
        config.AddBehavior<DeleteRouletteTypeAuthorization>(ServiceLifetime.Scoped);

        // Query Behaviors

        return config;
    }
}
